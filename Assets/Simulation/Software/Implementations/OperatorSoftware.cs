using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Operator: Software
    {
        private Dictionary<Message, FrameAction> myFrameActions = new Dictionary<Message, FrameAction>{
        };
        protected override Dictionary<Message, FrameAction> MyFrameActions
        {
            get => myFrameActions;
        }
        public Operator(int maxListenersNumber,Robot robot): base( ref robot)
        {
            robot.radio.maxListenersNumber = maxListenersNumber;
        }    
        public Operator(Robot robot): base(ref robot)
        {
            robot.radio.maxListenersNumber = 5;
        }    
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Operator;
            }
        }
        protected override void handleRequest(Frame message)
        {

        }
        protected override void handleService(Frame message)
        {
            if (message.message is Message.Subscribe)
            {
                attributedRobot.radio.AddListener(message.srcMac);
                Frame identifyMe = new Frame
                (
                    TransmissionType.Unicast,
                    DestinationRole.NoMatter,
                    MessageType.ACK,
                    Message.Subscribe,
                    destMac: message.srcMac
                );
                attributedRobot.radio.SendFrame(identifyMe);
            }
        }

        protected override void handleAcknowledge(Frame message)
        {

        }
        public void SendAllTransportToPosition(float x, float y, float z)
        {   
            Frame moveTo = new Frame
            (
                TransmissionType.Unicast,
                DestinationRole.Transporter,
                MessageType.Service,
                Message.MoveTo,
                payload: new Payload(new float[]{x,y,z})
            );
            attributedRobot.radio.NotifySubscribers(moveTo);
        }
    }
}