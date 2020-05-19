using Simulation.Common;
using Simulation.Utils;
using Simulation.World;
using System.Linq;
using UnityEngine;

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
            WaitForTask();
        }
        public void StartWorking()
        {
            Debug.Log("Start working");
            currentState = working;
        }
        public void WaitForTask() => currentState = waitingForTask;
        public void StartWaitingForOrder()
        {
            Debug.Log("Start waiting");
            currentState = waitingForOrder;
        }
        public Building FindBuilding() 
        {
            return FindObjectsOfType<Building>()
                   .OrderBy(x => Vector3.Distance(x.ClosestPoint(AttributedSoftware.Position), AttributedSoftware.Position))
                   .ToList()[0];
        }
        public void StartScheduler()
        {
            UseScheduler = true;
        }
        protected override void DoAction() 
        {
            currentState.DoAction();
        }
    }
}
