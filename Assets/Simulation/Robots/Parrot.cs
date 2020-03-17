using Simulation.Common;
using Simulation.Roles;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class Parrot : Robot
    {
        private Operator _role;
        public override Role role
        {
            get
            {
                return _role;
            }
        }
        public Parrot(Vector2 position, float radioRange, ref Medium ether): base(position,
        radioRange, ref ether)
        {
            _role = new Operator(this);
        }
    }
}