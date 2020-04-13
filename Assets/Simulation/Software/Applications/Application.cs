using System.Collections.Generic;
using Simulation.Utils;
using Simulation.Common;
using UnityEngine;
using System;
using Simulation.Components;

namespace Simulation.Software
{
    public abstract class Application : MonoBehaviour
    {
        protected CommunicationBasedApplicationState currentState;
        protected Dictionary<Message, Action<Frame>> ActionsOnRecive;
        public OperatingSystem AttributedSoftware;
        public Radio Radio { get; protected set; }

        public virtual void ReceiveFrame(Frame frame)
        {
            currentState.Receive(frame);
        }
        // Does it needed ? 
        public virtual void Activate() { }

        public void installOn(OperatingSystem system)
        {
            this.AttributedSoftware = system;
            Radio = system.attributedRobot.radio;
            initStates();
        }
        public abstract void initStates();
    }

}