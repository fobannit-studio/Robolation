using Simulation.Common;
using Simulation.Utils;
using Simulation.World;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
            if (frame.messageType is MessageType.Service && frame.message is Message.BuildNewBuilding)
            {
                StartBuilding();
                var response = new Frame(
                            TransmissionType.Unicast,
                            DestinationRole.Operator,
                            MessageType.ACK,
                            Message.BuildNewBuilding,
                            destMac: frame.srcMac);
                Application.AttributedSoftware.Radio.SendFrame(response);
            }
            else if (frame.messageType is MessageType.Request && frame.message is Message.FindFreeBuilders) 
            {
                var response = new Frame(
                            TransmissionType.Unicast,
                            DestinationRole.Operator,
                            MessageType.NACK,
                            Message.FindFreeBuilders,
                            destMac: frame.srcMac);
                Application.AttributedSoftware.Radio.SendFrame(response);
            }
            else if (frame.messageType is MessageType.NACK && frame.message is Message.FindFreeBuilders)
            {
                /// If builder was already found
                isWorking = false;
                Application.WaitForTask();
            }


        }

        private void StartBuilding()
        {
            isWorking = true;
            AttributedSoftware.attributedRobot.IterationsPassed = 0;
            currentBuilding = Application.FindBuilding();
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
        /// <summary>
        /// Checks if current building is built
        /// </summary>
        /// <returns></returns>
        private bool IsBuildingBuilt() 
        {
            foreach(var material in currentBuilding.GetSlotContainer().GetMax())
            {
                if (material.Value - currentBuilding.GetSlotContainer().GetContent()[material.Key] != 0) return false;
            }
            AttributedSoftware.attributedRobot.IterationsPassed = 0;

            return true;
        }
        public override void DoAction()
        {
            if (!isWorking) return;
            if (IsBuildingBuilt()) 
            {
                isWorking = false;
                Application.WaitForTask();
                return;
            }

            if (CheckMaterials())
                Build();
            else RequestMaterials();
        }
        private void Build() 
        {
            // Means that no more materials is needed
            requestSent = false;
            Debug.Log("Robot is starting to build");
            AttributedSoftware.attributedRobot.IterationsPassed++;
            if (AttributedSoftware.attributedRobot.IterationsPassed>= AttributedSoftware.attributedRobot.BuildIterations)
            {
                currentBuilding.Build(AttributedSoftware.attributedRobot.MaterialContainer);
                AttributedSoftware.attributedRobot.IterationsPassed=0;
            }
           
        }
    }
}