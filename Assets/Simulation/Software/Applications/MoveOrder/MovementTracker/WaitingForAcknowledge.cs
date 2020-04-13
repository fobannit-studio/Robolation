using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class WaitingForAcknowledge : CommunicationBasedApplicationState
    {
        public WaitingForAcknowledge(Application app) : base(app) { }
        public override void Send(Payload payload)
        { Debug.Log("Waiting for acknowledge. Cannot send new frame! "); }
        public override void Receive(Frame frame)
        { Debug.Log("Received Frame !"); }
    }
}