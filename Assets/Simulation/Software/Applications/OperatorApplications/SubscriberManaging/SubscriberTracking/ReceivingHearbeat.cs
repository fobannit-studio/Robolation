using Simulation.Common;
using UnityEngine;
namespace Simulation.Software
{
    /// <summary>Enter when don't receive heartbeat for a long time</summary>
    class ReceivingHeartbeat: CommunicationBasedApplicationState
    {
        // After some time, application should check, if subscription occured, 
        // and if no - start looking for subscriber
        public ReceivingHeartbeat(Application app): base(app) { }
        public override void Send()
        { 
            Debug.Log("Sending confirmation to subscriber");
        }
        public override void Receive(Frame frame)
        {
            var (x, y, z) = frame.payload;
            Application.AttributedSoftware.RoutingTable[(frame.SendingOS, frame.srcMac)] = (x, y, z);
            //Debug.Log($"Received heartbeat from {frame.srcMac}");
        }
    }
}