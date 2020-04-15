using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class Moving : CommunicationBasedApplicationState
    {
        public Moving(Application app) : base(app) { }
        public override void Send()
        { 
            Debug.Log("I come to point so sending Confirmation !");
        }
        public override void Receive(Frame frame)
        {
            Debug.Log("I should think if should change my direction !");
        }
    }
}