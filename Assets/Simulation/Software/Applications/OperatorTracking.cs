using Simulation.Utils;
using System.Collections.Generic;
namespace Simulation.Software
{
    class OperatorTracking: Application, IOperated
    {
        private Dictionary<Message, FrameAction> actions = new Dictionary<Message, FrameAction>
        {
            {Message.Subscribe, new SubscribeToOperatorAction()},
            {Message.Notify, new HeartbeatAction()}
        };
        protected override Dictionary<Message, FrameAction> Actions
        {
            get => actions;
        }

        protected override void Run() {
            SubscribeToOperator();
        }
        public void SubscribeToOperator()
        {
            Actions[Message.Subscribe].Call();
        }
    }
}