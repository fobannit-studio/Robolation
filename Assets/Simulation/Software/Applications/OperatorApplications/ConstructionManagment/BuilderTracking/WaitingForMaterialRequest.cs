using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Software
{
    internal class WaitingForMaterialRequest : CommunicationBasedApplicationState
    {
        private bool isWaitingForTranspReponse = false;
        private string requestedMaterial;
        private int requestedAmount;
        public WaitingForMaterialRequest(BuilderTracking app): base(app)
        { }

        public override void Receive(Frame frame)
        {
            if (isWaitingForTranspReponse && frame.message is Message.isFree && frame.messageType is MessageType.ACK) 
            {
                 isWaitingForTranspReponse = false;
                (Application as BuilderTracking).SendTransporterToBringMaterials(frame.srcMac, requestedMaterial, requestedAmount);
            }
            else if (frame.message is Message.BringMaterials) 
            {
                (string material, float amount, float _, float _) = frame.payload;
                requestedMaterial = material;
                requestedAmount = (int)amount;
                Debug.Log($"Received request for {material} in amount {amount}");
                var FindFreeTransporter = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Transporter,
                    MessageType.Request,
                    Message.isFree);
                foreach (var mac in (this.Application as BuilderTracking).ManagingApp.TransportersMacAddresses)
                {
                    isWaitingForTranspReponse = true;
                    FindFreeTransporter.destMac = mac;
                    AttributedSoftware.Radio.SendFrame(FindFreeTransporter);
                }
            }
        }
    }
}