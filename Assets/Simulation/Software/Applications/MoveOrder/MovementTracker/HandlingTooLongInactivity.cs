using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class HandlingTooLongInactivity : CommunicationBasedApplicationState
    {
        public HandlingTooLongInactivity(Application app) : base(app) { }
        public override void Send(Payload payload)
        { }
        public override void Receive(Frame frame)
        {

        }
    }
}