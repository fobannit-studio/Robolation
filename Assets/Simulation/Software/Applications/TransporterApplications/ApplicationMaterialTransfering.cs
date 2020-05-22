using Simulation.Common;
using Simulation.Utils;
using System;
namespace Simulation.Software
{
    class MaterialTransfering : Application
    {
        private TransportingMaterial transportingMaterial;
        private WaitingForTransportOrders waitingForOrder;
        private WaitingForMaterialInformation waitingForMaterialInfo;
        public override void initStates()
        {
            transportingMaterial = new TransportingMaterial(this);
            waitingForMaterialInfo = new WaitingForMaterialInformation(this);
            waitingForOrder = new WaitingForTransportOrders(this);
            currentState = waitingForOrder;
        }
        public void StartTransportingMaterial(string material, int amount) {
            currentState = transportingMaterial;
            transportingMaterial.StartTransporting(material, amount);
        }
        public void StartWaitingForOrder() => currentState = waitingForOrder;
        public void StartWaitingForMaterialInfo() => currentState = waitingForMaterialInfo;

        protected override bool receiveCondition(Frame frame)
        {
            return frame.message is Message.isFree || frame.message is Message.BringMaterials;
        }
    }
}
