using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Builder : OperatingSystem, IOperated
    {
        // IAction subscribeAction;
        // FrameAction subscribeAction;
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Builder;
            }
        }
        private Dictionary<Message, FrameAction> myFrameActions = new Dictionary<Message, FrameAction>{
            {Message.Subscribe, new SubscribeToOperatorAction()}
        };
        protected override Dictionary<Message, FrameAction> MyFrameActions
        {
            get => myFrameActions;
        }

        public Builder(Robot robot) : base(ref robot)
        {
            SubscribeToOperator();
        }
        public void SubscribeToOperator()
        {
            MyFrameActions[Message.Subscribe].Call();
        }
        public void SendAck()
        {

        }

    }
}