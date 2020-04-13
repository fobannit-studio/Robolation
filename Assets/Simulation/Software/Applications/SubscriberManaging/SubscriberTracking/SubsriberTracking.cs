using System.Collections;
using Simulation.Common;
using UnityEngine;
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
            Debug.Log("Before");
            StartCoroutine(Scheduler(2.0f));
            Debug.Log("After");
        }
        public override void ReceiveFrame(Frame frame)
        {
            isReceivingHeartbeat = true;
            currentState.Receive(frame);
        }
        public override void Activate()
        {
            currentState = receivingHeartbeat;
        }
        private IEnumerator Scheduler(float waitingForTime)
        {
            while(true)
            {
                yield return new WaitForSeconds(waitingForTime * Time.timeScale);
                currentState = isReceivingHeartbeat 
                              ? receivingHeartbeat
                              : lookingForMissingSubscriber;
                isReceivingHeartbeat = false;
                currentState.Send();
            }
        } 
    }
}