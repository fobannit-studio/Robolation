using Simulation.Common;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class IRB1100 : Robot
    {
        public override int BuildIterations => 5;

        protected override int cointainer_size => 60;
       
    }
}