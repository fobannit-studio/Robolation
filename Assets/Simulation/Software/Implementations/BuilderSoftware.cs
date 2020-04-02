using System.Collections.Generic;
using System.Collections;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class BuilderSoftware : OperatingSystem
    {
        protected override DestinationRole IReceive  => DestinationRole.Builder; 
        protected  override void LoadSoft() 
        {
            requiredSoft = new List<Application>
            {
               attributedRobot.gameObject.AddComponent<OperatorTracking>()
            };


        }

     
    }
}