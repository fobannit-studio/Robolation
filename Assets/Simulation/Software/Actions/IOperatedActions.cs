using Simulation.Utils;
using Simulation.Common;
using Simulation.Components;
namespace Simulation.Software
{
    public interface IOperated
    {
        void SubscribeToOperator();
        void SendAck();

    }

    class SubscribeToOperatorAction : FrameAction
    {
        protected override Message myMessage
        {
            get => Message.Subscribe;
        }
        public override void Call()
        {
            Frame findOperatorFrame = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Operator,
                MessageType.Service,
                this.myMessage
            );
            attributedSoftware.radio.SendFrame(findOperatorFrame);
        }
        protected override void handleFrame(Frame frame)
        {
            attributedSoftware.radio.AddListener(frame.srcMac);
            frame.destMac = frame.srcMac;
            frame.messageType = MessageType.ACK;
            attributedSoftware.radio.SendFrame(frame);
        }
    }

    class SendAckAction : IAction
    {
        Radio radio;
        public void DoAction()
        {

        }
    }
}