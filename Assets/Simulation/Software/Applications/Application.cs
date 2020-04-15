using System.Collections;
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
        public OperatingSystem AttributedSoftware;
        protected bool UseScheduler {get; set; } = false;
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
            if(UseScheduler) StartCoroutine(Scheduler(2.0f));

        }
        public abstract void initStates();

        protected virtual IEnumerator Scheduler(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime * Time.timeScale);
                DoAction();
            }
        }
        protected virtual void DoAction() { }

    }

}