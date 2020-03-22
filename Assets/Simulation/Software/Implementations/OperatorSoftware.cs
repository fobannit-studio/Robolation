using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Operator : OperatingSystem
    {
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Operator;
            }
        }
        private List<Application> reqiuredSoft = new List<Application>
        {
            OperatingSystem.tmpgameobj.AddComponent<SubscriberTracking>()
        };
        protected override List<Application> ReqiuredSoft
        {
            get => reqiuredSoft;
        }

        public Operator(int maxListenersNumber, Robot robot) : base(ref robot)
        {
            robot.radio.maxListenersNumber = maxListenersNumber;
        }
        public Operator(Robot robot) : base(ref robot)
        {
            robot.radio.maxListenersNumber = 5;
        }

        public void SendAllTransportToPosition(float x, float y, float z)
        {
            Frame moveTo = new Frame
            (
                TransmissionType.Unicast,
                DestinationRole.Transporter,
                MessageType.Service,
                Message.MoveTo,
                payload: new Payload(new float[] { x, y, z })
            );
            attributedRobot.radio.NotifySubscribers(moveTo);
        }
    }
}