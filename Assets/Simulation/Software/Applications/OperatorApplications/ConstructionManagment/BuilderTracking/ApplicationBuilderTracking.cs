using Simulation.Common;
using Simulation.World;
using UnityEngine;
using System.Collections.Generic;
using Simulation.Utils;
namespace Simulation.Software
{
    class BuilderTracking : Application
    {

        public Vector3 AdministratedBuilderPosition { get; private set; }
        private List<int> RobotsITrack = new List<int>();
        private CommunicationBasedApplicationState waitingForBuilderComeToPosition;
        private CommunicationBasedApplicationState waitingForMaterialRequest;
        private CommunicationBasedApplicationState sendingTransporterToGetMaterials;
        public Building AdministratedBuilding { get; set; }
        public override void initStates()
        {
            waitingForMaterialRequest = new WaitingForMaterialRequest(this);
            waitingForBuilderComeToPosition = new WaitingForBuilderComeToPosition(this);
            sendingTransporterToGetMaterials = new SendingTransporterToGetMaterials(this);
            currentState = waitingForBuilderComeToPosition;
            UseScheduler = true;
        }
        public void SendTransporterToMaterials(BuildingMaterial material) 
            => SendTransporterToMaterials(new List<BuildingMaterial> { material});
        public void SendTransporterToMaterials(List<BuildingMaterial> materials)
        {
            ((SendingTransporterToGetMaterials)sendingTransporterToGetMaterials).RequestMaterials(materials);
            currentState = sendingTransporterToGetMaterials;
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
        => RobotsITrack.Contains(frame.srcMac);

    }
}
