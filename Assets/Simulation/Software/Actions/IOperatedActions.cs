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
        public override void Send()
        {
            Frame findOperatorFrame = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Operator,
                MessageType.Service,
                this.myMessage
            );
            radio.SendFrame(findOperatorFrame);
        }
        public override void Receive(Frame frame)
        {
            radio.AddListener(frame.srcMac);
        }
    }

    // class SubscribeToOperatorAction : IAction
    // {
    //     private readonly Radio radio;
    //     public SubscribeToOperatorAction(Radio radio)
    //     {
    //         this.radio = radio;
    //     }
    //     public void DoAction()
    //     {
    //         Frame findOperatorFrame = new Frame(
    //            TransmissionType.Broadcast,
    //            DestinationRole.Operator,
    //            MessageType.Service,
    //            Message.Subscribe
    //        );
    //         radio.SendFrame(findOperatorFrame);
    //     }
    // }

    class SendAckAction : IAction
    {
        Radio radio;
        public void DoAction()
        {

        }
    }
}