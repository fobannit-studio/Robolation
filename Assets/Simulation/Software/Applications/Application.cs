using System.Collections;
using System.Collections.Generic;
using Simulation.Common;
using UnityEngine;
using Simulation.Components;
namespace Simulation.Software
{
    public abstract class Application : MonoBehaviour
    {
        protected float Frequency { get; set; } = 0.75f;
        private IEnumerator scheduler;
        protected CommunicationBasedApplicationState currentState;
        public RobotOperatingSystem AttributedSoftware;
        private bool useScheduler;
        protected bool UseScheduler {
            get => useScheduler;
            set 
            {
                if (value) StartCoroutine(scheduler);
                else StopCoroutine(scheduler);
                useScheduler = value;
            } 
        }
        public Radio Radio { get; protected set; }
        ///<summary>
        /// Indicates if this application can receive given frame
        ///<summary/>
        protected virtual bool receiveCondition(Frame frame) => true;
        public virtual void ReceiveFrame(Frame frame)
        {
            if(receiveCondition(frame))
            {
                BeforeReceive(frame);
                currentState.Receive(frame);
            }
        }
        public virtual void ReceiveFrame(Frame frame, out bool received)
        {
            if (receiveCondition(frame))
            {
                BeforeReceive(frame);
                currentState.Receive(frame, out received);
            }
            else received = false;
        }

        public T CreateAppBasedOnFrame<T>(Frame frame, Dictionary<int, T> registrator) where T : Application
           => CreateAppBasedOnFrame<T>(frame.srcMac, registrator);

        public T CreateAppBasedOnFrame<T>(int srcMac, Dictionary<int, T> registrator) where T : Application
        {
            if (registrator.ContainsKey(srcMac)) return registrator[srcMac];
            T newApp = AttributedSoftware.GameObject.AddComponent<T>();
            newApp.installOn(AttributedSoftware);
            registrator.Add(srcMac, newApp);
            return newApp;
        }
        public void installOn(RobotOperatingSystem system)
        {
            this.AttributedSoftware = system;
            Radio = system.attributedRobot.radio;
            scheduler = Scheduler(Frequency);
            initStates();
        }
        private IEnumerator Scheduler(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime * Time.timeScale);
                DoAction();
            }
        }
        public abstract void initStates();
        protected virtual void DoAction() 
        {
            currentState.DoAction();
        }
        protected virtual void BeforeReceive(Frame frame) { }

    }

}