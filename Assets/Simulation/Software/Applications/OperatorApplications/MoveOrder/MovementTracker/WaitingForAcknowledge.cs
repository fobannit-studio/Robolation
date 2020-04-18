using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class WaitingForAcknowledge : CommunicationBasedApplicationState
    {
        public WaitingForAcknowledge(MovementTracker app) : base(app) { }
        public override void Send(Payload payload)
        {
            Debug.Log("Waiting for acknowledge. Cannot send new frame! "); 
        }
        public override void Receive(Frame frame)
        {
            (float x, float y, float z) = frame.payload;            
            (Application.AttributedSoftware as OperatorSoftware).MoveOrder.FinishMoveOrderTracking(x,y,z);
            (Application as MovementTracker).ReturnControl(frame);
        }
    }
}