using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Transporter : OperatingSystem, IOperated
    {
        FrameAction subscribeAction;

        private Dictionary<Message, FrameAction> myFrameActions = new Dictionary<Message, FrameAction>{
            {Message.Subscribe, new SubscribeToOperatorAction()}
        };
        protected override Dictionary<Message, FrameAction> MyFrameActions
        {
            get => myFrameActions;
        }

        public Transporter(Robot robot) : base(ref robot)
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

        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Transporter;
            }
        }
        // protected override void handleRequest(Frame message)
        // {

        // }
        // protected override void handleService(Frame message)
        // {
            

        // }
        // protected override void handleAcknowledge(Frame message)
        // {

        // }
    }
}