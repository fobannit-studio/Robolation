using System.Collections.Generic;
using Simulation.Utils;
namespace Simulation.Software
{
    class BuilderSoftware : RobotOperatingSystem
    {
        protected override DestinationRole IReceive  => DestinationRole.Builder; 
        protected  override void LoadSoft() 
        {
            requiredSoft = new List<Application>
            {
               attributedRobot.gameObject.AddComponent<Movement>(),
               attributedRobot.gameObject.AddComponent<OperatorTracking>(),
               attributedRobot.gameObject.AddComponent<BuildingApplication>()
            };
        }
    }
}