using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Builder : Software, IOperated
    {
        // IAction subscribeAction;
        // FrameAction subscribeAction;
        private Dictionary<Message, FrameAction> myFrameActions = new Dictionary<Message, FrameAction>{
            {Message.Subscribe, new SubscribeToOperatorAction()}
        };
        protected override Dictionary<Message, FrameAction> MyFrameActions
        {
            get => myFrameActions;
        }

        public Builder(Robot robot): base(ref robot)
        {
            SubscribeToOperator();
        }
        public void SubscribeToOperator(){
            MyFrameActions[Message.Subscribe].Send();
        }
        public void SendAck()
        {

        }
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Builder;
            }
        }
        protected override void handleRequest(Frame message)
        {

        }
        protected override void handleService(Frame message)
        {

        }
        protected override void handleAcknowledge(Frame message)
        {

        }

    }
}