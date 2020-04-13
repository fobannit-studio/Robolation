using Simulation.Utils;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using System;
using System.Collections;
namespace Simulation.Software
{
    class MovementTracker : Application
    {
        // Contains record for every uncompleted move order
        private CommunicationBasedApplicationState readyToSendOrder;
        private CommunicationBasedApplicationState waitingForAcknowledge;
        private CommunicationBasedApplicationState handlingTooLongInactivity;
        public override void Activate()
        {
            currentState = readyToSendOrder;
        }
        public override void initStates()
        {
            readyToSendOrder = new ReadyToSendOrder(this);
            waitingForAcknowledge = new WaitingForAcknowledge(this);
            handlingTooLongInactivity = new HandlingTooLongInactivity(this);
        }
        
        public void SetWaitingForAck()
        {
            currentState = waitingForAcknowledge;
        }
        public void SetReadyToSendOrder()
        {
            currentState = readyToSendOrder;
        }
        private IEnumerator Scheduler(float time)
        {
            while(true)
            {
                yield return new WaitForSeconds(time * Time.timeScale);
            }
        }        
        // public void SendOrder()
        // {
        //     Frame frame = new Frame(
        //         TransmissionType.Unicast,
        //         DestinationRole.Transporter,
        //         MessageType.Service,
        //         Message.MoveTo,
        //         payload: new Payload(floatPayload: xyz));
        //     software.radio.NotifySubscribers(frame);
        // }
        // private void ReceiveACK(Frame frame)
        // {
        //     Debug.Log(frame);
        // }

    }
}
