using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Roles
{
    class Builder : Role
    {
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