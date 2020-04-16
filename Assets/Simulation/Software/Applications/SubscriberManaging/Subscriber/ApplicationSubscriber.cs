using System;
using System.Collections;
using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    ///<summary> 
    /// Waiting for subscriptions. When receive new subscription, 
    /// starts new process, that will track this instance
    /// </summary>
    class Subscriber : Application
    {
        bool sent = false;
        private CommunicationBasedApplicationState subscribing;
        protected override bool receiveCondition(Frame frame) => frame.message is Message.Subscribe || frame.message is Message.Heartbeat;
        public override void initStates()
        {
            subscribing = new Subscribing(this);
            currentState = subscribing;
        }
    }


}
