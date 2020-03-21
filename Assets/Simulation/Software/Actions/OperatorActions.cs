using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    class RegisterSubscriberAction : FrameAction
    {
        protected override Message myMessage
        {
            get => Message.Subscribe;
        }
        public override void Call()
        {
            Frame identifyMe = new Frame
            (
                TransmissionType.Unicast,
                DestinationRole.NoMatter,
                MessageType.ACK,
                Message.Subscribe,
                destMac: lastReceivedFrame.srcMac
            );
            attributedSoftware.radio.SendFrame(identifyMe);
        }
        protected override void handleFrame(Frame frame)
        {
            if (frame.messageType == MessageType.Service)
            {
                attributedSoftware.radio.AddListener(frame.srcMac);
                this.Call();
            }
            else if (frame.messageType == MessageType.ACK)
            {
                Debug.Log("Subscription suceed !");
            }
        }
    }
}