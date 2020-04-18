using Simulation.Common;
using Simulation.World;
using UnityEngine;
namespace Simulation.Software
{
    class BuilderTracking : Application
    {

        private CommunicationBasedApplicationState waitingForMaterialRequest;
        public Building AdministratedBuilding { get; set; }
        public override void initStates()
        {
            waitingForMaterialRequest = new WaitingForMaterialRequest(this);
            currentState = waitingForMaterialRequest;
        }
        public void GetControl() 
        {
            Debug.Log("Control received !");
        }
    }
}
