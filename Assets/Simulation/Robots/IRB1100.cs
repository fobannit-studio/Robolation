using Simulation.Common;
using Simulation.Roles;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class IRB1100 : Robot
    {
        private Role _role;
        public override Role role
        {
            get
            {
                return _role;
            }
        }
        public IRB1100(Vector2 position, float radioRange, ref Medium ether): base(position, radioRange, ref ether)
        {
            _role = new Builder(this);

        }
    }
}