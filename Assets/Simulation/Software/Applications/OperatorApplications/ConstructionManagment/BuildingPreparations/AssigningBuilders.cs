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
                    MessageType.Service,
                    Message.BuildNewBuilding,
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
            if( frame.messageType is MessageType.ACK && frame.message is Message.BuildNewBuilding && Application.BuildingsWithoutBuilders.Count > 0) 
            {
                Debug.Log(frame.srcMac);
                BuilderTracking ControlThread = Application.CreateAppBasedOnFrame(frame, Application.BuilderTrackingApplications);
                ControlThread.ManagingApp = this.Application;
                var buildingPosition = Application.BuildingsWithoutBuilders.Pop().ClosestPoint(AttributedSoftware.Position);
                builderTrackingThreads.Add(ControlThread);
                (AttributedSoftware as OperatorSoftware).MoveOrder.MoveToPosition(buildingPosition.x, buildingPosition.y, buildingPosition.z, ControlThread.GetControl, frame.srcMac);
            }
            else if(frame.message is Message.BringMaterials || frame.message is Message.isFree)
            {
                foreach(var thread in builderTrackingThreads) 
                {
                    thread.ReceiveFrame(frame);
                }
            }
        }
    }
}