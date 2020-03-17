using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Roles
{
    class Transporter : Role
    {
        public Transporter(Robot robot): base(ref robot)
        {
            robot.FindOperator();
        }
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Transporter;
            }
        }
        protected override void handleRequest(Frame message)
        {

        }
        protected override void handleService(Frame message)
        {

        }
    }
}