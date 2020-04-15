using UnityEngine;
using System.Collections;
using Simulation.Utils;
using Simulation.Common;
namespace Simulation.Software
{
    ///<summary> 
    /// While started, receiving heartbeat from subscriber and in case of no 
    /// frames during long time, 
    /// decides what to do next.
    ///</summary> 
    class SubscriberTracking: Application
    {
        private bool isReceivingHeartbeat { get; set; } = false;
        private CommunicationBasedApplicationState receivingHeartbeat;
        private CommunicationBasedApplicationState lookingForMissingSubscriber;
        public override void initStates()
        {
            lookingForMissingSubscriber = new LookingForMissingSubscriber(this);
            receivingHeartbeat = new ReceivingHeartbeat(this);
            currentState = receivingHeartbeat;
            UseScheduler = true;
        }

        protected override bool receiveCondition(Frame frame)
        {
            if(frame.message is Message.Heartbeat)
            {
                isReceivingHeartbeat = true;
                return true;
            }
            return false;
        }
        protected override void DoAction()
        {
                currentState = isReceivingHeartbeat 
                  ? receivingHeartbeat
                  : lookingForMissingSubscriber;
                isReceivingHeartbeat = false;
                currentState.Send();

        }
    }
}