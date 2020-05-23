using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    class TransportingMaterial : CommunicationBasedApplicationState
    {
        /// <summary>
        /// Position to which this transporeter should return after he will deliver his materials
        /// </summary>
        private Vector3 warehousePosition;
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
            warehousePosition = AttributedSoftware.Position;
            Send();
        }
        public override void Receive(Frame frame)
        {
            // Receive frame, that confirms, that he come to builder
            if ( frame.message is Message.BringMaterials && frame.messageType is MessageType.ACK) 
            {
                Debug.Log("Hurra !  I come to my builder ");
                GiveMaterialToBuilder();
                (Application as MaterialTransfering).ReturnToWarehouse(warehousePosition);
            }
        }
        private void GiveMaterialToBuilder() 
        {
            Debug.Log("Giving material to builder");
        }
    }
}
