using Simulation.Common;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class Spot : Robot
    {
        public Spot(Vector2 position, float radioRange, ref Medium ether): base(position, radioRange, ref ether)
        {}
    }
}