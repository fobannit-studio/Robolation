using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    class Transporter : Software
    {
        public Transporter(Robot robot): base(ref robot)
        {
            FindOperator();
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