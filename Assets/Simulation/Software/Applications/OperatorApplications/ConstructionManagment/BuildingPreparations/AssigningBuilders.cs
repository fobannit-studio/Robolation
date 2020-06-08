using Simulation.Common;
using Simulation.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation.Software
{
    class AssigningBuilders : CommunicationBasedApplicationState
    {
        private new BuildingPreparation Application;
        private List<BuilderTracking> builderTrackingThreads = new List<BuilderTracking>();
        
        public AssigningBuilders(Application app): base(app)
        {
            Application = app as BuildingPreparation;
        }
        /// <summary>
        /// Looking for free builders among subscribed builders and send frame tothem to check if they are free;
        /// </summary>
        public override void Send()
        {
            foreach( var builderMac in Application.BuildersMacAddresses) 
            {
                var findFreeBuilders = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Builder,
                    MessageType.Request,
                    Message.FindFreeBuilders,
                    destMac: builderMac
                    );
                AttributedSoftware.Radio.SendFrame(findFreeBuilders);
            }
        }
        /// <summary>
        /// Called on receive frame from builder
        /// </summary>
        /// <param name="frame"></param>
        public override void Receive(Frame frame)
        {
            if( frame.messageType is MessageType.ACK && frame.message is Message.FindFreeBuilders && Application.BuildingsWithoutBuilders.Count > 0 ) 
            {
                AssignBuilderToBuilding(frame.srcMac);
            }
            else if (frame.messageType is MessageType.ACK && frame.message is Message.FindFreeBuilders && Application.BuildingsWithoutBuilders.Count == 0)
            {
                var response = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Builder,
                    MessageType.NACK,
                    Message.FindFreeBuilders,
                    destMac: frame.srcMac);
                AttributedSoftware.Radio.SendFrame(response);
            }
            else if(frame.message is Message.BringMaterials || frame.message is Message.isFree || frame.message is Message.StartTransporting)
            {
                foreach(var thread in builderTrackingThreads) 
                {
                    thread.ReceiveFrame(frame, out bool received);
                    if (received) break;
                }
            }
        }

        private void AssignBuilderToBuilding(int builderMac)
        {
            Debug.Log(builderMac);
            BuilderTracking ControlThread = Application.CreateAppBasedOnFrame(builderMac, Application.BuilderTrackingApplications);
            ControlThread.ManagingApp = Application;
            ControlThread.AdministratedBuilderMac = builderMac;
            var building = Application.BuildingsWithoutBuilders[Application.BuildingsWithoutBuilders.Count - 1];
            Application.BuildingsWithoutBuilders.Remove(building);
            var buildingPosition = building.ClosestPoint(AttributedSoftware.Position);
            builderTrackingThreads.Add(ControlThread);
            (AttributedSoftware as OperatorSoftware).MoveOrder.MoveToPosition(buildingPosition.x, buildingPosition.y, buildingPosition.z, ControlThread.GetControl, builderMac);
        }

    }
}