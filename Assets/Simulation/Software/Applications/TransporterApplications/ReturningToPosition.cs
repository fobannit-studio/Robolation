using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    class ReturningToPosition : CommunicationBasedApplicationState
    {
        private Vector3 Dest;
        public ReturningToPosition(Application application) : base(application)
        { }

        public override void Receive(Frame frame)
        {
            Debug.Log("Returning to warehouse. No response");
        }
        public void StartMoving(Vector3 dest)
        {
            Dest = dest;
            AttributedSoftware.attributedRobot.MoveOrder(dest);
        }
        public override void DoAction()
        {
            var dist = Vector2.Distance(new Vector2(AttributedSoftware.Position.x, AttributedSoftware.Position.z),
                                 new Vector2(Dest.x, Dest.z));
            if (dist < 1)
            {
                AttributedSoftware.attributedRobot.MoveOrder(AttributedSoftware.Position);
                (Application as MaterialTransfering).StartWaitingForOrder();
            }
        }
    }
}
