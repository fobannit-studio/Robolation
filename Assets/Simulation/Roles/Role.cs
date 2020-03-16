using Simulation.Common;
using UnityEngine;
namespace Simulation.Roles{
    abstract class Role{
        public abstract void ReceiveFrame(Frame message);
    }
}