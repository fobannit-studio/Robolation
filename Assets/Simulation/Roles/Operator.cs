using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Roles{
    class Operator: Role{
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
            }
        }
        public void SendAllTransportToPosition((float x, float y, float z) position)
        {   
            Frame toSend = new Frame
            (
                DestinationRole.Transporter,
                MessageType.Service,
                Message.MoveTo,
                position
            );
            attributedRobot.radio.NotifySubscribers(toSend);
        }
    }
}