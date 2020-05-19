using Simulation.Common;
using Simulation.Utils;
using Simulation.World;
using UnityEngine;

namespace Simulation.Software
{
    internal class Working : CommunicationBasedApplicationState
    {
        private new BuildingApplication Application;
        private bool isWorking = false;
        private Building currentBuilding;

        public Working(Application app) : base(app)
        {
            Application = app as BuildingApplication;

        }
        public override void Receive(Frame frame)
        {
            var ack = isWorking ? MessageType.NACK : MessageType.NACK;
            isWorking = true;
            currentBuilding = Application.FindBuilding();
            Application.StartScheduler();
            var response = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Operator,
                    ack,
                    Message.BuildNewBuilding,
                    destMac: frame.srcMac);
            Application.AttributedSoftware.Radio.SendFrame(response);
        }

        public override void DoAction()
        {
            Build();
        }
        private void Build() 
        {
            Debug.Log("Building");
        }
    }
}