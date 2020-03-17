using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Roles{
    class Operator: Role{        
        protected override DestinationRole IReceive
        {
            get
            {
                return DestinationRole.Operator;
            }
        }
        protected override void handleRequest(Frame message)
        {

        }
        protected override void handleService(Frame message)
        {

        }
        // protected void handleService(Frame frame)
        // {
        //     if( frame.message is Message.Subscribe)
        //     {
        //         this.registerSubscriber(frame.srcId);
        //     }
        // }
        // protected void handleRequest()
        // public void registerSubscriber(int subscriberId)
        // {
        //     this.robot.Subscribers.Add(subscriberId);
        //     Frame notifyAboutSuccess = new Frame(
        //         TransmissionType.Unicast,
        //         DestinationRole.Broadcast,
        //         MessageType.Service,
        //         Message.Subscribe,
        //         this.robot.macAddress,
        //         subscriberId,
        //         0
        //     );
        //     this.robot.NotifySubscribers(notifyAboutSuccess);
        // }
    }
}