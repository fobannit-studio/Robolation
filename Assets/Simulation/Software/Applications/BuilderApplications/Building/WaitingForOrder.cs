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

        public WaitingForOrder(Application application) : base(application)
        {
        }

        public override void Receive(Frame frame)
        {
            // Not used 
            //if(frame.message is Message.MoveTo) 
            //{
            //    (float x,float y, float z) = frame.payload;
            //    ((BuildingApplication)Application).StartWorking();
            //    Application.AttributedSoftware.attributedRobot.MoveOrder(new Vector3(x, y, z));
                
            //}
        }
    }
}