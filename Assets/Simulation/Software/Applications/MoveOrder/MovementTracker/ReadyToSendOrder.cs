using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class ReadyToSendOrder : CommunicationBasedApplicationState
    {
        public ReadyToSendOrder(Application app) : base(app) { }
        public override void Send(Payload payload)
        {
            // Sends first subscriber 
            // in the macTable to the position  
            var frame = new Frame
            (
                TransmissionType.Unicast,
                DestinationRole.Transporter,
                MessageType.Service,
                Message.MoveTo,
                payload: payload
            );
            AttributedSoftware.radio.NotifySubscribers(frame);
        }
        public override void Receive(Frame frame)
        {

        }
    }
}