using UnityEngine;
using Simulation.Common;
using System.Collections.Generic;
using System.Collections;
using Simulation.Utils;
namespace Simulation.Software
{
    class Movement : Application
    {

        private CommunicationBasedApplicationState movement;
        private CommunicationBasedApplicationState waiting;
        protected override bool receiveCondition(Frame frame) => frame.message is Message.MoveTo;
        public override void initStates()
        {
            movement = new Moving(this);
            currentState = waiting;
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
