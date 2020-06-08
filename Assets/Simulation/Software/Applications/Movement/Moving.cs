using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class Moving : CommunicationBasedApplicationState
    {
        private new Movement Application;
        public Moving(Movement app) : base(app)
            => Application = app as Movement;
        public override void Send()
        {
            if (Vector2.Distance(new Vector2(Application.Destination.x, Application.Destination.z), 
                                 new Vector2(Application.AttributedSoftware.Position.x, Application.AttributedSoftware.Position.z)) < 1)
            {
                AttributedSoftware.attributedRobot.MoveOrder(AttributedSoftware.attributedRobot.Position);
                Debug.Log($"I come to point so sending Confirmation !  ({AttributedSoftware.Radio.macAddress})");
                float[] dest = new[] { Application.Destination.x, Application.Destination.y, Application.Destination.z };
                var reportGoalReached = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Operator,
                    MessageType.ACK,
                    Message.MoveTo,
                    destMac: Application.OrderAuthor,
                    payload: new Payload(dest));
                AttributedSoftware.Radio.SendFrame(reportGoalReached);
                Application.SetWaitingState();
            }
        }
        public override void Receive(Frame frame)
        {
            Debug.Log("I should think if should change my direction !");
        }
    }
}