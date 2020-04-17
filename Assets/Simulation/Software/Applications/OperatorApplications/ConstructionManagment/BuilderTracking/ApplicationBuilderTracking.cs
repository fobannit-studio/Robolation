using Simulation.Common;
using Simulation.World;
using UnityEngine;
namespace Simulation.Software
{
    class BuilderTracking : Application
    {

        private CommunicationBasedApplicationState managingBuilderToPosition;
        //private CommunicationBasedApplicationState waitingForBuilderArriving;
        //private CommunicationBasedApplicationState waitingForBuilderFeedback;
        public Building AdministratedBuilding { get; set; }
        public override void initStates()
        {
            managingBuilderToPosition = new ManagingBuilderToPosition(this);
            currentState = managingBuilderToPosition;
        }
    }
}
