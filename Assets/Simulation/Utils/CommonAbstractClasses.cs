using Simulation.Components;
using Simulation.Common;
using Simulation.Software;
namespace Simulation.Utils
{
    abstract class FrameAction
    {
        protected Frame lastReceivedFrame;
        protected OperatingSystem attributedSoftware;
        protected  bool ignoreListenersAmount=false;
     
        public void installOn(OperatingSystem attributedSoftware)
        {
            this.attributedSoftware = attributedSoftware;
        }

        public void Receive(Frame frame){
            lastReceivedFrame = frame;
            handleFrame(frame);
   
        }
        public void Call()
        {
            if (ignoreListenersAmount || attributedSoftware.attributedRobot.radio.ListenerAmount>0)   
                Execute();
            
        }
        public virtual void Execute()
        {

        }
        protected abstract void handleFrame(Frame frame);

    }
}