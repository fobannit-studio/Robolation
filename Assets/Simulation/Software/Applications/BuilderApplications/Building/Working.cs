using Simulation.Common;
using Simulation.Utils;
using Simulation.World;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation.Software
{
    internal class Working : CommunicationBasedApplicationState
    {
        private new BuildingApplication Application;
        private bool isWorking = false;
        private Building currentBuilding;
        private KeyValuePair<BuildingMaterial, int> currentMaterial;
        private bool requestSent = false;

        public Working(Application app) : base(app)
        {
            Application = app as BuildingApplication;

        }
        public override void Receive(Frame frame)
        {
                var ack = isWorking ? MessageType.NACK : MessageType.NACK;
                isWorking = true;
                currentBuilding = Application.FindBuilding();
                Application.StartScheduler();
                var response = new Frame(
                        TransmissionType.Unicast,
                        DestinationRole.Operator,
                        ack,
                        Message.BuildNewBuilding,
                        destMac: frame.srcMac);
                Application.AttributedSoftware.Radio.SendFrame(response);
        }

        private bool CheckMaterials() 
        {
            //ConcurrentDictionary<BuildingMaterial, int> cont = currentBuilding.GetFull();
            SlotContainer buildingContainer = currentBuilding.GetSlotContainer();
            foreach(var requiredMaterial in currentBuilding.GetFull())
            {
                int requiredAmount = requiredMaterial.Value - buildingContainer.GetContent()[requiredMaterial.Key];
                if (requiredAmount != 0) 
                {
                    currentMaterial = new KeyValuePair<BuildingMaterial, int>(
                        requiredMaterial.Key, 
                        requiredMaterial.Value - buildingContainer.GetContent()[requiredMaterial.Key]);
                    return AttributedSoftware.attributedRobot.MaterialContainer.CanTake(currentMaterial.Key, 1);
                }
            }
            return true;
        }
        private void RequestMaterials() 
        {
            if (!requestSent)
            {
                requestSent = true;
                Debug.Log($"Requesting {currentMaterial.Key} in amount {currentMaterial.Value}. Request by {AttributedSoftware.Radio.macAddress}");
                var payload = new Payload(new float[] { currentMaterial.Value, 0, 0 }, currentMaterial.Key.Type);
                var requestMaterial = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Operator,
                    MessageType.Request,
                    Message.BringMaterials,
                    destMac: AttributedSoftware.OperatorMac,
                    payload: payload
                    );
                AttributedSoftware.Radio.SendFrame(requestMaterial);
            }
        }
        public override void DoAction()
        {
            if (CheckMaterials())
                Build();
            else RequestMaterials();
        }
        private void Build() 
        {
            // Means that no more materials is needed
            requestSent = false;
            Debug.Log("Robot is starting to build");
            currentBuilding.Build(AttributedSoftware.attributedRobot.MaterialContainer);
         
          

        }
    }
}