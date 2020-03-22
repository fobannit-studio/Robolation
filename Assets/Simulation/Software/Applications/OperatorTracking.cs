using Simulation.Utils;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace Simulation.Software
{
    class OperatorTracking: Application, IOperated
    {
        private IEnumerator coroutine;
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
            coroutine = Heartbeat(2.0f);
            StartCoroutine(coroutine);
        }
        public void SubscribeToOperator()
        {
            Actions[Message.Subscribe].Call();
        }
        public IEnumerator Heartbeat(float waitTime)
        {
            while(true)
            {
                yield return new WaitForSeconds(waitTime);
                Actions[Message.Notify].Call();
            }

        }
    }
}