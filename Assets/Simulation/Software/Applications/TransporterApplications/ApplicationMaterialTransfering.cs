using Simulation.Common;
using Simulation.Utils;
using Simulation.World;
using System.Linq;
using UnityEngine;

namespace Simulation.Software
{
    class MaterialTransfering : Application
    {
        private TransportingMaterial transportingMaterial;
        private WaitingForTransportOrders waitingForOrder;
        private WaitingForMaterialInformation waitingForMaterialInfo;
        private ReturningToPosition returningToPosition; 
        public override void initStates()
        {
            transportingMaterial = new TransportingMaterial(this);
            waitingForMaterialInfo = new WaitingForMaterialInformation(this);
            waitingForOrder = new WaitingForTransportOrders(this);
            returningToPosition = new ReturningToPosition(this);
            StartWaitingForOrder();
        }
        public void StartTransportingMaterial(string material, int amount) {
            currentState = transportingMaterial;
            transportingMaterial.StartTransporting(material, amount);
        }
        public void StartWaitingForOrder()
        {
            currentState = waitingForOrder;
            UseScheduler = false;
        }
        public void StartWaitingForMaterialInfo()
        {
            currentState = waitingForMaterialInfo;
            UseScheduler = false;
        }
        public void ReturnToWarehouse(Vector3 warehousePosition) 
        {
            currentState = returningToPosition;
            returningToPosition.StartMoving(warehousePosition);
            UseScheduler = true;
        }

        protected override bool receiveCondition(Frame frame)
        {
            return frame.message is Message.isFree || frame.message is Message.BringMaterials;
        }
        public Vector3 FindWarehouse()
        {
            return FindObjectsOfType<Warehouse>()
                   .OrderBy(x => Vector3.Distance(x.ClosestPoint(AttributedSoftware.Position), AttributedSoftware.Position))
                   .ToList()[0].transform.position;
        }
      
    }
}
