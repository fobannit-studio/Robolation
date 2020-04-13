using Simulation.Common;
using UnityEngine;

namespace Simulation.Software
{
    /// <summary>Enter when don't receive heartbeat for a long time</summary>
    class LookingForMissingSubscriber: CommunicationBasedApplicationState
    {
        public LookingForMissingSubscriber(Application app): base(app) { }
        public override void Send()
        {
            Debug.Log("Subscriber missed!  Looking for subscriber !");
        }
        public override void Receive(Frame frame)
        {

        }
    }
}