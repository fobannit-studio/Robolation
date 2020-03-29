using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class OperatorSoftware : OperatingSystem
    {
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Operator;
            }
        }


        private readonly List<Application> reqiuredSoft;
        public override List<Application> ReqiuredSoft
        {
            get => reqiuredSoft;
        }

        public OperatorSoftware(int maxListenersNumber, Robot robot) : base(ref robot)
        {
            robot.radio.maxListenersNumber = maxListenersNumber;

            reqiuredSoft = new List<Application>
            {
               attributedRobot.gameObject.AddComponent<SubscriberTracking>(),
               attributedRobot.gameObject.AddComponent<MoveOrder>()

            };
            InstallSoft();



        }
        public OperatorSoftware(Robot robot) : this(5, robot)
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
                payload: new Payload(new float[] { x, y, z })
            );
            attributedRobot.radio.NotifySubscribers(moveTo);
        }



    }
}