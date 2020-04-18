using Simulation.Common;
using UnityEngine;
namespace Simulation.Software
{
    internal class WaitingForBuilderComeToPosition : CommunicationBasedApplicationState
    {
        public WaitingForBuilderComeToPosition(Application application) : base(application)
        {
        }
        public override void Send()
        {
            Debug.Log("Check if builder come and if no - send new frame !");
        }

        public override void Receive(Frame frame)
        {
            throw new System.NotImplementedException();
        }
    }
}