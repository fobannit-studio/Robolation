using System.Collections.Generic;
using System.Collections;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Builder : OperatingSystem
    {
        private IEnumerator courutine;
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Builder;
            }
        }
        private List<Application> reqiuredSoft = new List<Application>
        {
            new OperatorTracking()
        };
        protected override List<Application> ReqiuredSoft
        {
            get => reqiuredSoft;
        }
        public Builder(Robot robot) : base(ref robot)
        { }
    }
}