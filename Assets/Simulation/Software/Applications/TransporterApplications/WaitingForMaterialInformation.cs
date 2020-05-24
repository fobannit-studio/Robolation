using Simulation.Common;
using Simulation.Software;
using Simulation.Utils;
using System;
using UnityEngine;
namespace Simulation.Software
{
    class WaitingForMaterialInformation : CommunicationBasedApplicationState
    {
        public WaitingForMaterialInformation(Application application) : base(application)
        {
        }

        public override void Receive(Frame frame)
        {
            Debug.Log($"Transporter received {frame}");
            if (frame.message is Message.isFree) 
            {
                var confirmationOfUnAvailibility = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Operator,
                MessageType.NACK,
                Message.isFree,
                destMac: frame.srcMac
                );
                AttributedSoftware.Radio.SendFrame(confirmationOfUnAvailibility);
            }
            else if (frame.message is Message.BringMaterials) 
            {
                (string material, float amount, _, _) = frame.payload;
                var buildingMaterial = BuildingMaterial.existingMaterials[material];
                AttributedSoftware.attributedRobot.MaterialContainer.GetContent().TryGetValue(buildingMaterial, out int amountInContainer);
                bool isSuccess = AttributedSoftware.PickUpFromWarehouse(material, Math.Abs((int)amount - amountInContainer));
                if (isSuccess)
                    (Application as MaterialTransfering).StartTransportingMaterial(material, (int)amount);
                else
                    Debug.Log("Not enough materials on warehouse !");
            }
        }
    }
}
