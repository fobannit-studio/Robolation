using System.Collections.Generic;
using System.Collections;
using Simulation.Common;
using Simulation.Utils;
using System.Linq;
using UnityEngine;
namespace Simulation.Software
{
    class TransporterSoftware : RobotOperatingSystem
    {
        protected override DestinationRole IReceive => DestinationRole.Transporter;
        protected override void LoadSoft()
        {
            requiredSoft = new List<Application>
            {    
                attributedRobot.gameObject.AddComponent<Movement>(),
                attributedRobot.gameObject.AddComponent<OperatorTracking>(),
                attributedRobot.gameObject.AddComponent<MaterialTransfering>()
            };
        }
        public Vector3 FindClosestWarehouse() 
        {
            return Simulation.Warehouses
                              .OrderBy(x => Vector3.Distance(x.ClosestPoint(Position), Position))
                              .ToList()[0].ClosestPoint(Position); 
        }
    }
}