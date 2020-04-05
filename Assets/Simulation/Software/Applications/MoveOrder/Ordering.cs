using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class Ordering : CommunicationBasedApplicationState
    {
        public Ordering(Application app) : base(app) { }
        public override void Send()
        { }
        public override void Receive(Frame frame)
        { }
    }
}