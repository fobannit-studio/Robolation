using Simulation.Common;
using Simulation.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation.Software
{
    internal class SendingTransporterToGetMaterials : CommunicationBasedApplicationState
    {
        public string Material;
        public int Amount;
        public int TransporterMac;

        public SendingTransporterToGetMaterials(Application application) : base(application)
        { }
        public override void Send()
        {
            Debug.Log("Sending to transporter frame with material information");
            var payload = new Payload(Amount, Material);
            var materialRequest = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Transporter,
                MessageType.Service,
                Message.BringMaterials,
                destMac: TransporterMac,
                payload: payload);
            AttributedSoftware.Radio.SendFrame(materialRequest);
        }
        /// <summary>
        /// Handle message from transporter that can indicate if he take material successfully 
        /// or if he take material unsuccesfully
        /// </summary>
        /// <param name="frame"></param>
        public override void Receive(Frame frame)
        {
            // Warehouse have enough materials, so transporter picked them and go to builder
            if (frame.message is Message.BringMaterials && frame.messageType is MessageType.ACK)
            {
                var position = (Application as BuilderTracking).AdministratedBuilderPosition;
                Debug.Log($"Order to move to {position}");
                (AttributedSoftware as OperatorSoftware).MoveOrder.MoveToPosition(position, GetControl, frame.srcMac);
            }
            else if (frame.message is Message.BringMaterials && frame.messageType is MessageType.NACK) 
            {
                // Warehouse have not enough materials, so transporter picked them and go to builder
                Debug.Log("Not enough materials on this warehouse");
            }
        }
        private void GetControl(Frame frame)
        {
            Debug.Log("Transporter arrived to Builder !");
            var arriveConfirm = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Transporter,
                MessageType.ACK,
                Message.BringMaterials,
                destMac: frame.srcMac);
            AttributedSoftware.Radio.SendFrame(arriveConfirm);
            (Application as BuilderTracking).StartWaitForMaterialRequst();
        }
    }
}