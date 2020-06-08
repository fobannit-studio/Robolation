using System.Collections.Generic;
using Simulation.Utils;
using System;
namespace Simulation.Software
{
    class OperatorSoftware : RobotOperatingSystem
    {
        protected override DestinationRole IReceive => DestinationRole.Operator;
        protected override void LoadSoft()
        {
            attributedRobot.radio.maxListenersNumber = 100;
            requiredSoft = new List<Application>
            {
               attributedRobot.gameObject.AddComponent<Subscriber>(),
               attributedRobot.gameObject.AddComponent<MoveOrder>(),
               attributedRobot.gameObject.AddComponent<BuildingPreparation>()
            };
        }
        public Subscriber Subscriber { get => requiredSoft[0] as Subscriber; }
        public MoveOrder MoveOrder { get => requiredSoft[1] as MoveOrder; }
        public BuildingPreparation BuildingPreparation { get => requiredSoft[2] as BuildingPreparation; }
    }
}