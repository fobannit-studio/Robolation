using Simulation.Utils;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Simulation.Common;
using System;


namespace Simulation.Software
{
    class MoveOrder : Application
    {
        private float[] xyz = new float[] { 2.28f, 0.3f, 12.44f };
        public override void Activate()
        {
            ActionsOnRecive = new Dictionary<Message, Action<Frame>>
            {
                {Message.MoveTo, ReceiveACK }

            };
        }

        public void SendOrder()
        {
            Frame frame = new Frame(
                TransmissionType.Unicast, 
                DestinationRole.Transporter, 
                MessageType.Service, 
                Message.MoveTo,
                payload: new Payload(floatPayload:xyz));
            software.radio.NotifySubscribers(frame);
        }
        private void ReceiveACK(Frame frame)
        {
            Debug.Log(frame);
        }

    }
}
