using Simulation.Common;
using Simulation.Utils;
namespace Simulation.Software
{
    public class LookingForOperator : CommunicationBasedApplicationState
    {
        public LookingForOperator(Application app) : base(app) { }
        public override void Send()
        {
            Frame findOperatorFrame = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Operator,
                MessageType.Service,
                Message.Subscribe
            );
            radio.SendFrame(findOperatorFrame);
        }
        public override void Receive(Frame frame)
        {
            radio.AddListener(frame.srcMac);
            frame.destMac = frame.srcMac;
            frame.messageType = MessageType.ACK;
            ((OperatorTracking) Application).OperatorMac = frame.srcMac;
            radio.SendFrame(frame);
        }
    }
}