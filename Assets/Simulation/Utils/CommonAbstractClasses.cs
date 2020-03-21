using Simulation.Components;
using Simulation.Common;
namespace Simulation.Utils
{
    abstract class FrameAction
    {
        protected Radio radio;
        protected abstract Message myMessage
        {
            get;   
        }
        public void assignRadio(Radio radio)
        {
            this.radio = radio;
        }
        public bool isMyFrame(Frame frame)
        {
            return frame.message == myMessage;
        }
        public abstract void Send();
        public abstract void Receive(Frame action); 
    }
}