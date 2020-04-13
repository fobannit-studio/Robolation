using UnityEngine;
using Simulation.Common;
using Simulation.Utils;
namespace Simulation.Software
{
    public class Subscribed : CommunicationBasedApplicationState
    {
        public Subscribed(Application app) : base(app) { }

        public override void Send()
        {
            Frame heartbeatFrame = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Operator,
                MessageType.Heartbeat,
                Message.Notify,
                destMac: ((OperatorTracking) Application).OperatorMac
            );
            radio.SendFrame(heartbeatFrame);
        }
        public override void Receive(Frame frame)
        {
            if (frame.message != Message.Notify)
            {
                Debug.Log("Error!!!!!");
            }
            else
            {
                Debug.Log("Operator answered");
            }
        }
    }
}