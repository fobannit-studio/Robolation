using Simulation.Common;
using Simulation.Robots;
using Simulation.Utils;
using System.Linq;
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
            warehousePosition = (Application as MaterialTransfering).FindWarehouse();

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
        private Robot FindBuilder()
        {
            var robot = Simulation.Robots
                       .Where(x => x.radio.macAddress == (Application as MaterialTransfering).TargetBuilder)
                       .FirstOrDefault();
            return robot;
        }
        private void GiveMaterialToBuilder() 
        {
            Debug.Log("Giving material to builder");
            var builder = FindBuilder();
            if (builder)
            {
                var to_give = AttributedSoftware.attributedRobot.MaterialContainer.GetContent();
                foreach (var item in to_give)
                {
                    this.AttributedSoftware.attributedRobot.PutObject(builder.MaterialContainer, item.Key, item.Value);
                    Debug.LogFormat("Transfered {0} {1} to builder", item.Key.Type, item.Value);
                }

            }

        }
    }
}
