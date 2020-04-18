using UnityEngine;
using Simulation.Common;
using System.Collections.Generic;
using System.Collections;
using Simulation.Utils;
namespace Simulation.Software
{
    public class Movement : Application
    {
        public Vector3 Destination { get; set; }
        public int OrderAuthor { get; set; }
        private CommunicationBasedApplicationState movement;
        private CommunicationBasedApplicationState waiting;
        protected override bool receiveCondition(Frame frame) => frame.message is Message.MoveTo;
        public override void initStates()
        {
            movement = new Moving(this);
            waiting = new Waiting(this);
            UseScheduler = true;
            currentState = waiting;
        }
        protected override void DoAction() 
        {
            currentState.Send();
        }
        public void SetMovingState()
        {
            currentState = movement;
        }
        public void SetWaitingState()
        {
            currentState = waiting;
        }
    }
}
