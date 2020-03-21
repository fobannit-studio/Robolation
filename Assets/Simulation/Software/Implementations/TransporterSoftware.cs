using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Transporter : Software, IOperated
    {
        IAction subscribeAction;
        public Transporter(Robot robot): base(ref robot)
        {
            subscribeAction = new SubscribeToOperatorAction(robot.radio);
            SubscribeToOperator();

        }
        public void SubscribeToOperator(){
            subscribeAction.DoAction();
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