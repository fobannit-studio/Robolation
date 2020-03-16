using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Roles{
    class Operator: Role{
        Robot robot;
        public Operator(Robot robot)
        {
            this.robot = robot;
        }
        public Operator(){}
        public override void ReceiveFrame(Frame message)
         {
            bool isForMe = message.destinationRole is DestinationRole.Operator || message.destinationRole is DestinationRole.Broadcast;
            if(isForMe)
             {
                Debug.Log($"{this.GetType().Name} received message {message}");
                // if (message.message is Message.Subscribe)
                // {
                //     registerSubscriber(message.srcMac);
                // }
             }
         }
        // public void registerSubscriber(int subscriberId)
        // {
        //     // this.robot.Subscribers.Add(subscriberId);
        //     // Frame notifyAboutSuccess = new Frame(
        //     //     TransmissionType.Unicast,
        //     //     DestinationRole.Broadcast,
        //     //     MessageType.Service,
        //     //     Message.Subscribe,
        //     //     this.robot.macAddress,
        //     //     subscriberId,
        //     //     0
        //     // );
        //     // this.robot.NotifySubscribers(notifyAboutSuccess);
        // }
    }
}