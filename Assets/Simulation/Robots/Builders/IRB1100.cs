using Simulation.Common;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class IRB1100 : Robot
    {
        public IRB1100(Vector2 position, float radioRange, ref Medium ether): base(position, radioRange, ref ether)
        {}
    }
}