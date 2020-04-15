using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Simulation.Utils;
using Simulation.Common;
namespace Simulation.Software
{
    class MovementTracker : Application
    {
        private CommunicationBasedApplicationState readyToSendOrder;
        private CommunicationBasedApplicationState waitingForAcknowledge;
        private CommunicationBasedApplicationState handlingTooLongInactivity;
        public override void initStates()
        {
            readyToSendOrder = new ReadyToSendOrder(this);
            waitingForAcknowledge = new WaitingForAcknowledge(this);
            handlingTooLongInactivity = new HandlingTooLongInactivity(this);
            currentState = readyToSendOrder;
        }
        protected override bool receiveCondition(Frame frame) 
        => frame.message is Message.MoveTo;
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
    }
}
