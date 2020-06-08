using Simulation.Common;
using Simulation.Utils;
using UnityEngine;

namespace Simulation.Software
{
    class WaitForNewWarehousePosition : CommunicationBasedApplicationState
    {
        public WaitForNewWarehousePosition(MaterialTransfering app) : base (app) 
        { }
        public override void Receive(Frame frame)
        {
            if (frame.messageType is MessageType.Request) 
            {
                (float x, float y, float z) = frame.payload;
                Debug.Log($"Received frame with order to move to {(x,y,z)}");
            }
        }

        public override void Send()
        {
            base.Send();
        }
    }
}
