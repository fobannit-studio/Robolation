using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class ReadyToSendOrder : CommunicationBasedApplicationState
    {
        public ReadyToSendOrder(MovementTracker app) : base(app) { }
        public override void Send(Payload payload)
        {
            // Sends first subscriber 
            // in the macTable to the position  
            var frame = new Frame
            (
                TransmissionType.Unicast,
                DestinationRole.Builder,
                MessageType.Service,
                Message.MoveTo,
                payload: payload,
                destMac: (Application as MovementTracker).TargetsMac
            );
            // TODO change on unicast
            AttributedSoftware.Radio.SendFrame(frame);
            (Application as MovementTracker).SetWaitingForAck();
        }
        public override void Receive(Frame frame)
        {

        }
    }
}