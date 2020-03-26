using Simulation.Utils;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Simulation.Common;
using System;

namespace Simulation.Software
{
    class OperatorTracking : Application
    {
        private IEnumerator coroutine;

        public OperatorTracking()

        {

            ActionsOnRecive = new Dictionary<Message, Action<Frame>>
            {

                {Message.Subscribe, registerOperator},
                {Message.Notify, recieveHeartbeatResponse}
            };
        }
        protected override void Run()
        {
            FindOperator();

            coroutine = Heartbeat(2.0f);
            StartCoroutine(coroutine);
        }

        private void FindOperator()
        {
            Frame findOperatorFrame = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Operator,
                MessageType.Service,
                Message.Subscribe
            );
            software.radio.SendFrame(findOperatorFrame);
        }

        private void heartbeat()
        {
            if (software == null) return;
            Frame heartbeatFrame = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Operator,
                MessageType.Heartbeat,
                Message.Notify,
                destMac: software.OperatorMac
            );
            software.radio.SendFrame(heartbeatFrame);
        }

        private void recieveHeartbeatResponse(Frame frame)
        {
            Debug.Log("Operator answered");
        }
        private void registerOperator(Frame frame)
        {
            software.radio.AddListener(frame.srcMac);
            software.OperatorMac = frame.srcMac;
            frame.destMac = software.OperatorMac;
            frame.messageType = MessageType.ACK;
            software.radio.SendFrame(frame);

        }

        private IEnumerator Heartbeat(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime * Time.timeScale);
                heartbeat();
            }

        }
    }
}