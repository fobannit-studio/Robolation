using Simulation.Common;
using Simulation.Utils;

namespace Simulation.Software
{
    internal class Working : CommunicationBasedApplicationState
    {
        private new BuildingApplication Application;
        public Working(Application app) : base(app)
        {
            Application = app as BuildingApplication;
        }

        public override void Receive(Frame frame)
        {
            var response = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Operator,
                    MessageType.NACK,
                    Message.BuildNewBuilding,
                    destMac: frame.srcMac
                );
            Application.AttributedSoftware.Radio.SendFrame(response);

        }
    }
}