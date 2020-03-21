using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Builder : Software
    {
        public Builder(Robot robot): base(ref robot)
        {
            FindOperator();
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