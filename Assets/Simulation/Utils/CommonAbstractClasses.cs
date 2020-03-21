using Simulation.Components;
using Simulation.Common;
using Simulation.Software;
namespace Simulation.Utils
{
    abstract class FrameAction
    {
        protected Frame lastReceivedFrame;
        protected OperatingSystem attributedSoftware;
        protected abstract Message myMessage
        {
            get;   
        }
        public void installOn(OperatingSystem attributedSoftware)
        {
            this.attributedSoftware = attributedSoftware;
        }
        public bool isMyFrame(Frame frame)
        {
            return frame.message == myMessage;
        }
        public void Receive(Frame frame){
            lastReceivedFrame = frame;
            handleFrame(frame);
        }

        public abstract void Call();
        protected abstract void handleFrame(Frame frame);

    }
}