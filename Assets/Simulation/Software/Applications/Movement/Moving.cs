using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class Moving : CommunicationBasedApplicationState
    {
        private new Movement Application;
        public Moving(Application app) : base(app)
            => Application = app as Movement;
        public override void Send()
        { 
            if (Application.Destination == Application.AttributedSoftware.Position)
                Debug.Log("I come to point so sending Confirmation !");
        }
        public override void Receive(Frame frame)
        {
            Debug.Log("I should think if should change my direction !");
        }
    }
}