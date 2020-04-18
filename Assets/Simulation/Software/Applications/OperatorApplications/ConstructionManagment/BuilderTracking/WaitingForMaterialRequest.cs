using Simulation.Common;
using UnityEngine;
namespace Simulation.Software
{
    internal class WaitingForMaterialRequest : CommunicationBasedApplicationState
    {
        public WaitingForMaterialRequest(Application app): base(app)
        { }

        public override void Receive(Frame frame)
        {
            Debug.Log("Received");
        }
    }
}