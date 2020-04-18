using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class Waiting : CommunicationBasedApplicationState
    {
        private new Movement Application;

        public Waiting(Application app) : base(app)
        => this.Application = app as Movement;
        public override void Send()
        {
            Debug.Log("Nothing to send!");
        }
        public override void Receive(Frame frame)
        {
            Debug.Log($"Received frame  {frame}. Start moving");
            ((Movement)Application).SetMovingState();
            (float x,float y,float z) = frame.payload;
            Application.Destination = new Vector3(x, y, z);
            AttributedSoftware.attributedRobot.MoveOrder(Application.Destination);
            Application.OrderAuthor = frame.srcMac;
            Application.SetMovingState();
        }

    }
}