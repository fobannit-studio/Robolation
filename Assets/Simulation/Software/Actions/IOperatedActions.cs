using Simulation.Utils;
using Simulation.Common;
using Simulation.Components;
using System.Timers;
using System.Collections;
using UnityEngine;
namespace Simulation.Software
{
    public interface IOperated
    {
        void SubscribeToOperator();
        // IEnumerator Heartbeat(float waitTime);
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
            attributedSoftware.OperatorMac = frame.srcMac; 
            frame.destMac = attributedSoftware.OperatorMac;
            frame.messageType = MessageType.ACK;
            attributedSoftware.radio.SendFrame(frame);
        }
    }

    class HeartbeatAction: FrameAction
    {
        protected override Message myMessage
        {
            get => Message.Notify;
        }
        public override void Call()
        {
            Debug.Log("Here");
            if(attributedSoftware == null) return;
            Frame heartbeatFrame = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Operator,
                MessageType.Heartbeat,
                this.myMessage,
                srcMac: this.attributedSoftware.OperatorMac
            );
            Debug.Log("Here 2");
            attributedSoftware.radio.SendFrame(heartbeatFrame);
            Debug.Log("But not here");
        }
        protected override void handleFrame(Frame frame)
        {

        }
        
    }
}