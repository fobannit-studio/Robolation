using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Roles{
    class Operator: Role{
        public Operator(int maxSubscribersNumber, Robot robot): base(robot)
        {
            robot.maxSubscribersNumber = maxSubscribersNumber;
        }    
        public Operator(Robot robot): base(robot)
        {
            robot.maxSubscribersNumber = 5;
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
                attributedRobot.addSubscriber(message.srcMac, message.payload.Item1, message.payload.Item2);
            }
        }
        public void SendAllTransportToPosition((float x, float y) position)
        {
            Frame toSend = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Transporter,
                MessageType.Service,
                Message.MoveTo,
                attributedRobot.macAddress,
                position
            );
            attributedRobot.NotifySubscribers(toSend);
        }
    }
}