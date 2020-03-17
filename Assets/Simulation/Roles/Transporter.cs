using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Roles
{
    class Transporter : Role
    {
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