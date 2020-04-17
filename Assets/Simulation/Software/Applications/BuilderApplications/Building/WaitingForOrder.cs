using UnityEngine;
using Simulation.Common;
using Simulation.Utils;
namespace Simulation.Software
{
    internal class WaitingForOrder : CommunicationBasedApplicationState
    {
        public WaitingForOrder(Application application) : base(application)
        {
        }

        public override void Receive(Frame frame)
        {
            if(frame.message is Message.MoveTo) 
            {
                (float x,float y, float z) = frame.payload;
                Application.AttributedSoftware.attributedRobot.MoveOrder(new Vector3(x, y, z));
            }
        }
    }
}