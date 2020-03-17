using Simulation.Common;
using Simulation.Roles;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class IRB1100 : Robot
    {
        private Role _role = new Builder();
        protected override Role Role
        {
            get
            {
                return _role;
            }
        }
        public IRB1100(Vector2 position,ref Medium ether): base(position, ref ether)
        {

        }
    }
}