using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Simulation.Utils;
using Simulation.Common;
namespace Simulation.Software
{
    public class MovementTracker : Application
    {
        private CommunicationBasedApplicationState readyToSendOrder;
        private CommunicationBasedApplicationState waitingForAcknowledge;
        /// <summary>
        /// Called when movement is finished, to return control to application
        /// </summary>
        public Action<Frame> ReturnControl;
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
