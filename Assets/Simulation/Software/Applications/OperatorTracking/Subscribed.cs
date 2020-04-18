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
            var pos = Application.AttributedSoftware.Position;
            var heartbeatFrame = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Operator,
                MessageType.Heartbeat,
                Message.Heartbeat,
                destMac: ((OperatorTracking) Application).OperatorMac,
                payload: new Payload(new float[] {pos.x, pos.y, pos.z})
            );
            //Debug.Log($"Sending {heartbeatFrame}");
            radio.SendFrame(heartbeatFrame);
        }
        public override void Receive(Frame frame)
        {
            if (frame.message != Message.Heartbeat)
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