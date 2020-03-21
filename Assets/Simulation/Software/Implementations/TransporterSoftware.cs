using System.Collections.Generic;
using System.Collections;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Transporter : OperatingSystem
    {
        FrameAction subscribeAction;
        private List<Application> reqiuredSoft = new List<Application>
        {
            new OperatorTracking()
        };
        protected override List<Application> ReqiuredSoft
        {
            get => reqiuredSoft;
        }

        public Transporter(Robot robot) : base(ref robot)
        { }
        
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Transporter;
            }
        }
    }
}