using Simulation.Common;
using Simulation.Roles;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class Parrot : Robot
    {
        public Parrot(Vector2 position, float radioRange, ref Medium ether): base(position,
        radioRange, ref ether)
        {}
    }
}