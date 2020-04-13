using Simulation.Common;
using Simulation.Components;
namespace Simulation.Software
{
    public abstract class CommunicationBasedApplicationState
    {
        public Application application;
        protected OperatingSystem software {get => application.software;}
        protected Radio radio;
        public CommunicationBasedApplicationState(Application application)
        {
            this.application = application;
            this.radio = application.Radio;
        }
        public abstract void Send();
        public abstract void Receive(Frame frame);
    }
}