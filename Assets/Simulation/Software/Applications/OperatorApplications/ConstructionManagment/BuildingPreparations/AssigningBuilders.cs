﻿using Simulation.Common;
using Simulation.Utils;
namespace Simulation.Software
{
    class AssigningBuilders : CommunicationBasedApplicationState
    {
        private new BuildingPreparation Application;
        public AssigningBuilders(Application app): base(app)
        {
            Application = app as BuildingPreparation;
        }
        /// <summary>
        /// Looking for free builders among subscribed builders and send frame them to check if they are free;
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
        public override void Receive(Frame frame)
        {
            if( frame.messageType is MessageType.ACK && frame.message is Message.BuildNewBuilding && Application.BuildingsWithoutBuilders.Count > 0) 
            {
                Application.CreateAppBasedOnFrame(frame, Application.BuilderTrackingApplications);
                var buildingPosition = Application.BuildingsWithoutBuilders.Pop().transform.position;
                (AttributedSoftware as OperatorSoftware).MoveOrder.MoveToPosition(buildingPosition.x, buildingPosition.y, buildingPosition.z);
            }
        }
    }
}