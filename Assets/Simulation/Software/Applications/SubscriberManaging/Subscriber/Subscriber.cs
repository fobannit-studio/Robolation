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
        CommunicationBasedApplicationState Subscribing;

        public override void initStates()
        {
            Subscribing = new Subscribing(this);
        }
        public override void Activate()
        {
            currentState = Subscribing;
        }
    }


}
