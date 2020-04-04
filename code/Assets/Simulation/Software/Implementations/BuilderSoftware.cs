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
        private IEnumerator courutine;
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Builder;
            }
        }
        private readonly List<Application> reqiuredSoft;
        public override List<Application> ReqiuredSoft
        {
            get => reqiuredSoft;
        }
        public BuilderSoftware(Robot robot) : base(ref robot)
        {

            reqiuredSoft = new List<Application>
            {
               attributedRobot.gameObject.AddComponent<OperatorTracking>()
            };
            InstallSoft();


        }
    }
}