using Simulation.Common;
using Simulation.World;
using UnityEngine;
using System.Collections.Generic;
using Simulation.Utils;
using System.Linq;

namespace Simulation.Software
{
    class BuilderTracking : Application
    {

        public BuildingPreparation ManagingApp;
        public Vector3 AdministratedBuilderPosition { get; private set; }
        private List<int> RobotsITrack = new List<int>();
        private WaitingForBuilderComeToPosition waitingForBuilderComeToPosition;
        private WaitingForMaterialRequest waitingForMaterialRequest;
        private SendingTransporterToGetMaterials sendingTransporterToGetMaterials;
        public Building AdministratedBuilding { get; set; }
        public override void initStates()
        {
            waitingForMaterialRequest = new WaitingForMaterialRequest(this);
            waitingForBuilderComeToPosition = new WaitingForBuilderComeToPosition(this);
            sendingTransporterToGetMaterials = new SendingTransporterToGetMaterials(this);
            currentState = waitingForBuilderComeToPosition;
            UseScheduler = true;
        }

        public void SendTransporterToBringMaterials(int transpMac, string material, int amount) 
        {
            sendingTransporterToGetMaterials.TransporterMac = transpMac;
            sendingTransporterToGetMaterials.Material = material;
            sendingTransporterToGetMaterials.Amount = amount;
            currentState = sendingTransporterToGetMaterials;
            currentState.Send();
            
        }
        public void StartWaitForMaterialRequst() => currentState = waitingForMaterialRequest;
        public void GetControl(Frame frame) 
        {
            UseScheduler = false;
            (float x, float y, float z) = frame.payload;
            AdministratedBuilderPosition = new Vector3(x, y, z);
            Debug.Log("Control received !");
            var startBuild = new Frame(
                            TransmissionType.Unicast,
                            DestinationRole.Builder,
                            MessageType.Service,
                            Message.BuildNewBuilding,
                            destMac: frame.srcMac
                            );
            Radio.SendFrame(startBuild);
            RobotsITrack.Add(frame.srcMac);
            StartWaitForMaterialRequst();
        }
        protected override bool receiveCondition(Frame frame)
        {
            return (ManagingApp.TransportersMacAddresses.Contains(frame.srcMac)
                    || ManagingApp.BuildersMacAddresses.Contains(frame.srcMac));
        }
            //RobotsITrack.Contains(frame.srcMac);

    }
}
