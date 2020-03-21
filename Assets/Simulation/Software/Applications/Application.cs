using System.Collections.Generic;
using Simulation.Utils;
using Simulation.Common;
namespace Simulation.Software
{
    abstract class Application
    {
        protected abstract Dictionary<Message, FrameAction> Actions
        {
            get;
        }
        protected virtual void Run()
        { }
        public void ReceiveFrame(Frame frame)
        {
            foreach (KeyValuePair<Message, FrameAction> action in Actions)
            {
                if (action.Value.isMyFrame(frame))
                    action.Value.Receive(frame);
            }
        }
        public void installOn(OperatingSystem system)
        {
            foreach (KeyValuePair<Message, FrameAction> action in Actions)
            {
                // Install each action on reqired software
                action.Value.installOn(system);
            }
            Run();
        }
    }
}