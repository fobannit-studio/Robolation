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
        public override void Activate()
        {
            software.radio.MacTableCapasityChanged += MacTableCapasityChanged;
            currentState = lookingForOperator;
            StartCoroutine(Scheduler(2.0f));
        }
        public override void initStates()
        {
            lookingForOperator = new LookingForOperator(this);
            subscribed = new Subscribed(this);
        }
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

        private IEnumerator Scheduler(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime * Time.timeScale);
                currentState.Send();
            }
        }
        public override void ReceiveFrame(Frame frame)
        {
            currentState.Receive(frame);
        }
    }
}