using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    class TransportingMaterial : CommunicationBasedApplicationState
    {
        public TransportingMaterial(Application app) : base(app)
        { }

        public override void Send()
        {
            var confirmTransportingReadiness = new Frame(
                 TransmissionType.Unicast,
                 DestinationRole.Operator,
                 MessageType.ACK,
                 Message.BringMaterials,
                 destMac: AttributedSoftware.OperatorMac
                );
            AttributedSoftware.Radio.SendFrame(confirmTransportingReadiness); 
        }

        public void StartTransporting(string material, int amount)
        {
            Debug.Log($"Start trasnporting {material} in amount {amount}");
            Send();
        }
        public override void Receive(Frame frame)
        {
            Debug.Log("Currently transporter in transporting state");
        }
    }
}
