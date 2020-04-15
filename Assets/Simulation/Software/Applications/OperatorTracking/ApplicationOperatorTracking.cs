using UnityEngine;
using System;
using System.Collections;
using Simulation.Common;
using Simulation.Components;
using Simulation.Utils;
namespace Simulation.Software
{
    class OperatorTracking : Application
    {
        public int OperatorMac { get; set; }
        private LookingForOperator lookingForOperator;
        private Subscribed subscribed;
        public override void initStates()
        {
            lookingForOperator = new LookingForOperator(this);
            subscribed = new Subscribed(this);
            AttributedSoftware.radio.MacTableCapasityChanged += MacTableCapasityChanged;
            currentState = lookingForOperator;
            UseScheduler = true;
        }
        ///<summary>
        /// Event called on change of radio mac table
        ///<summary/>
        private void MacTableCapasityChanged(object radio, MacTableCapacityChangedEventArgs args)
        {
            if (args.IsSubscription)
            {
                currentState = subscribed;
            }
            else
            {
                currentState = lookingForOperator;
            }
        }
        protected override void DoAction()
        {
            currentState.Send();
        }
    }
}