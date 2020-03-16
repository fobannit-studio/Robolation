using Simulation.Common;
using Simulation.Roles;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class Parrot : Robot
    {
        private Role _role;
        protected override Role Role
        {
            get
            {
                return _role;
            }
        }
        public Parrot(Vector2 position, ref Medium ether): base(position, ref ether)
        {
            Debug.Log("In Parrot");
            _role = new Operator();
        }
    }
}