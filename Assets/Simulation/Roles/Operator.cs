using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Roles{
    class Operator: Role{
        public override void ReceiveFrame(Frame message)
         {
            bool isForMe = message.destinationRole is DestinationRole.Operator || message.destinationRole is DestinationRole.Broadcast;
            if(isForMe)
             {
                Debug.Log($"{this.GetType().Name} received message {message}");
             }
         }
    }
}