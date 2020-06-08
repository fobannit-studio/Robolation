using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    internal class WaitingForMaterialRequest : CommunicationBasedApplicationState
    {
        private bool isWaitingForTranspReponse = false;
        private int waitingForConfirmFromMac = -1;
        private int waiting = -1;
        private string requestedMaterial;
        private int requestedAmount;
        public WaitingForMaterialRequest(BuilderTracking app): base(app)
        { }

        public override void Receive(Frame frame, out bool received)
        {
            received = true;
            if (isWaitingForTranspReponse && frame.message is Message.isFree && frame.messageType is MessageType.ACK)
            {
                isWaitingForTranspReponse = false;
                var orderStart = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Transporter,
                    MessageType.Request,
                    Message.StartTransporting,
                    destMac: frame.srcMac,
                    payload: new Payload((Application as BuilderTracking).AdministratedBuilderMac));
                waitingForConfirmFromMac = frame.srcMac;
                AttributedSoftware.Radio.SendFrame(orderStart);
            }
            else if (frame.srcMac == waitingForConfirmFromMac && frame.message is Message.StartTransporting && frame.messageType is MessageType.ACK)
            {
                // Here could be a problem because i don't check for MAC
                waitingForConfirmFromMac = -1;
                (Application as BuilderTracking).SendTransporterToBringMaterials(frame.srcMac, requestedMaterial, requestedAmount);
            }
            else if (frame.message is Message.BringMaterials
                     && frame.messageType is MessageType.Request
                     && frame.srcMac == (Application as BuilderTracking).AdministratedBuilderMac)
            {
                Debug.Log($"Received frame {frame}");
                (string material, float amount, _, _) = frame.payload;
                requestedMaterial = material;
                requestedAmount = (int)amount;
                Debug.Log($"Received request for {material} in amount {amount} from {frame.srcMac}");
                isWaitingForTranspReponse = true;
            }
            else received = false;
        }
        public override void DoAction()
        {
            if (isWaitingForTranspReponse)
            {
                var FindFreeTransporter = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Transporter,
                    MessageType.Request,
                    Message.isFree);
                foreach (var mac in (this.Application as BuilderTracking).ManagingApp.TransportersMacAddresses)
                {
                    FindFreeTransporter.destMac = mac;
                    AttributedSoftware.Radio.SendFrame(FindFreeTransporter);
                }
            }
            if (waitingForConfirmFromMac != -1 && waiting > 2) 
            {
                waiting = 0;
                waitingForConfirmFromMac = -1;
                isWaitingForTranspReponse = true;
            }
            else if (waitingForConfirmFromMac != -1)
            {
                waiting += 1;
            }
        }
    }
}