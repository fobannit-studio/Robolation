using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    internal class WaitingForTask : CommunicationBasedApplicationState
    {
        private new BuildingApplication Application;
        public WaitingForTask(Application app) : base(app)
        {
            Application = app as BuildingApplication;
        }

        public override void Receive(Frame frame)
        {
            if(frame.message is Message.FindFreeBuilders && frame.messageType is MessageType.Request) 
            {
                 Application.StartWorking();
                //Debug.Log("Yo-ho-ho i butylka roma !");
                var response = new Frame(
                        TransmissionType.Unicast,
                        DestinationRole.Operator,
                        MessageType.ACK,
                        Message.FindFreeBuilders,
                        destMac: frame.srcMac);
                Application.AttributedSoftware.Radio.SendFrame(response);
            }
        }
    }
}