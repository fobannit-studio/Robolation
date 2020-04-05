using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class Moving : CommunicationBasedApplicationState
    {
        public Moving(Application app) : base(app) { }
        public override void Send()
        { }
        public override void Receive(Frame frame)
        { }
    }
}