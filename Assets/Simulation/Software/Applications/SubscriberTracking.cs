using System.Collections.Generic;
using Simulation.Utils;
namespace Simulation.Software
{
    class SubscriberTracking: Application
    {
        private Dictionary<Message, FrameAction> actions = new Dictionary<Message, FrameAction>
        {
            {Message.Subscribe, new RegisterSubscriberAction()},
            {Message.Notify, new TrackSubscriberAction()}
        };
        protected override Dictionary<Message, FrameAction> Actions
        {
            get => actions;
        }
    }
}