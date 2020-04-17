using Simulation.Common;
namespace Simulation.Software
{
    class ManagingBuilderToPosition : CommunicationBasedApplicationState
    {
        public ManagingBuilderToPosition(Application app) : base(app)
        { }
        public override void Receive(Frame frame)
        {
            throw new System.NotImplementedException();
        }
    }
}