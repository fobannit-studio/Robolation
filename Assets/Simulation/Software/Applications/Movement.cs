using Simulation.Utils;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Simulation.Common;
using System;

namespace Simulation.Software
{
    class Movement : Application
    {

        public override void Activate()
        {
            ActionsOnRecive = new Dictionary<Message, Action<Frame>>
            {
                {Message.MoveTo, ReceiveMoveOrder }
            };

           
        }

        private void SendACK(Frame frame)
        {
            Frame response = new Frame(
               TransmissionType.Unicast,
               DestinationRole.Operator,
               MessageType.ACK,
               Message.MoveTo,
               destMac: frame.srcMac);

            software.radio.SendFrame(response);
        }
        private void ReceiveMoveOrder(Frame frame)
        {
            var coords = frame.payload.floatPayload;
            Debug.Log(software.attributedRobot.transform.position);
            software.attributedRobot.MoveOrder(new Vector3(coords[0], coords[1], coords[2]));
            SendACK(frame);
        }

    }
}
