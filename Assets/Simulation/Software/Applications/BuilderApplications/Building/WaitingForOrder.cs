using UnityEngine;
using Simulation.Common;
using Simulation.Utils;
namespace Simulation.Software
{
    internal class WaitingForOrder : CommunicationBasedApplicationState
    {
        public WaitingForOrder(BuildingApplication app) : base(app)
        {
            Application = app as BuildingApplication;
        }

        public override void Receive(Frame frame)
        {
            //if(frame.message is Message.MoveTo) 
            //{
            //    (float x,float y, float z) = frame.payload;
            //    ((BuildingApplication)Application).StartWorking();
            //    Application.AttributedSoftware.attributedRobot.MoveOrder(new Vector3(x, y, z));
                
            //}
        }
    }
}