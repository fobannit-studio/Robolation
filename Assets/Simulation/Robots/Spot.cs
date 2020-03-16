using Simulation.Common;
using Simulation.Roles;
using Simulation.World;
using UnityEngine;
namespace Simulation.Robots
{
    class Spot : Robot
    {
        private Role _role = new Transporter();
        protected override Role Role
        {
            get
            {
                return _role;
            }
        }
        public Spot(Vector2 position, ref Medium ether): base(position, ref ether)
        {

        }
    }
}