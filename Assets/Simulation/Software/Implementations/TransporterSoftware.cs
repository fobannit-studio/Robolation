using System.Collections.Generic;
using System.Collections;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class TransporterSoftware : RobotOperatingSystem
    {
        protected override DestinationRole IReceive => DestinationRole.Transporter;
        protected override void LoadSoft()
        {
            requiredSoft = new List<Application>
           {    attributedRobot.gameObject.AddComponent<Movement>(),
                attributedRobot.gameObject.AddComponent<OperatorTracking>()
           };
        }
    }
}