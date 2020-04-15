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
        ///<summary>
        /// Indicates if this application can receive given frame
        ///<summary/>
        protected virtual bool receiveCondition(Frame frame) => true;
        public virtual void ReceiveFrame(Frame frame)
        {
            if(receiveCondition(frame))
                currentState.Receive(frame);
        }
        public void installOn(OperatingSystem system)
        {
            this.AttributedSoftware = system;
            Radio = system.attributedRobot.radio;
            initStates();
        }
        public abstract void initStates();
    }

}