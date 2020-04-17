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
        public float[] Position { get; set;  }

        public override void initStates()
        {
            readyToSendOrder = new ReadyToSendOrder(this);
            waitingForAcknowledge = new WaitingForAcknowledge(this);
            UseScheduler = true;
            currentState = readyToSendOrder;
        }

        protected override void DoAction()
        {
            currentState.Send(new Payload(Position));
        }
        protected override bool receiveCondition(Frame frame) 
        => frame.message is Message.MoveTo;
        public void SetWaitingForAck()
        {
            UseScheduler = false;
            currentState = waitingForAcknowledge;
        }
        public void SetReadyToSendOrder()
        {
            UseScheduler = true;
            currentState = readyToSendOrder;
        }
      
    }
}
