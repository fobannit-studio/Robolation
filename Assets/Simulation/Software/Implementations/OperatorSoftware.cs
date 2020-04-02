using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class OperatorSoftware : OperatingSystem
    {
        protected override DestinationRole IReceive => DestinationRole.Operator;



        protected override void LoadSoft()
        {
            attributedRobot.radio.maxListenersNumber = 5;

            requiredSoft = new List<Application>
            {
               attributedRobot.gameObject.AddComponent<SubscriberTracking>(),
               attributedRobot.gameObject.AddComponent<MoveOrder>()

            };
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