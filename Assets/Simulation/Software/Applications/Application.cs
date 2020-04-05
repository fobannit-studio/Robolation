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
        public OperatingSystem software;
        public Radio Radio { get; protected set; }

        public virtual void ReceiveFrame(Frame frame)
        {
            currentState.Receive(frame);
            // if (ActionsOnRecive.ContainsKey(frame.message))
            // {
            //     Action<Frame> action = ActionsOnRecive[frame.message];
            //     action(frame);
            // }
        }
        public virtual void Activate() { }

        public void installOn(OperatingSystem system)
        {
            this.software = system;
            Radio = system.attributedRobot.radio;
            initStates();
        }
        public abstract void initStates();
    }

}