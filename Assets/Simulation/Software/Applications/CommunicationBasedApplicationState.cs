using Simulation.Common;
using Simulation.Components;
using UnityEngine;
namespace Simulation.Software
{
    public abstract class CommunicationBasedApplicationState
    {
        
        public Application Application;
        protected RobotOperatingSystem AttributedSoftware { get => Application.AttributedSoftware; }
        protected Radio radio;
        public CommunicationBasedApplicationState(Application application)
        {
            Application = application;
            radio = application.Radio;
        }
        public virtual void Send() { Debug.Log("Should send predefined frame"); }
        public virtual void Send(Payload payload) { Debug.Log("Should send frame with payload"); }
        public abstract void Receive(Frame frame);
        public virtual void DoAction() { }
    }
}