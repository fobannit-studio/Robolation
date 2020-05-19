using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
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
            Debug.Log("Yo-ho-ho i butylka roma x 2 !");
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