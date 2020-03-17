using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Roles
{
    class Builder : Role
    {
        public Builder(Robot robot): base(robot)
        {
            robot.FindOperator();
        }
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Builder;
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