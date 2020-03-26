using System.Collections.Generic;
using System.Collections;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class TransporterSoftware : OperatingSystem
    {
        private readonly List<Application> reqiuredSoft;
        public override List<Application> ReqiuredSoft
        {
            get => reqiuredSoft;
        }

        public TransporterSoftware(Robot robot) : base(ref robot)
        { 
           reqiuredSoft = new List<Application>
            {
                 attributedRobot.gameObject.AddComponent<Movement>(),
                attributedRobot.gameObject.AddComponent<OperatorTracking>()
              
            };
            InstallSoft();

        }
        
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Transporter;
            }
        }
    }
}