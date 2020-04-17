using Simulation.Common;
using Simulation.Utils;
namespace Simulation.Software
{
    class BuildingApplication : Application
    {
        private CommunicationBasedApplicationState waitingForTask;
        private CommunicationBasedApplicationState waitingForOrder;
        private CommunicationBasedApplicationState working;
        protected override bool receiveCondition(Frame frame)
            => frame.message is Message.BuildNewBuilding;
        public override void initStates()
        {
            waitingForTask = new WaitingForTask(this);
            waitingForOrder = new WaitingForOrder(this);
            working = new Working(this);
            currentState = waitingForTask;
        }
        public void StartWorking() => currentState = working;
        public void StopWorking() => currentState = waitingForTask;
        public void StartWaitingForOrder() => currentState = waitingForOrder;
    }
}
