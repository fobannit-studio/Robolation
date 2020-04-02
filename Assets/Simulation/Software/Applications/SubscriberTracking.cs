using System;
using System.Collections;
using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using UnityEngine;



namespace Simulation.Software
{
    class SubscriberTracking : Application
    {
        bool sent = false;
        public override void Activate()
        {
     
            ActionsOnRecive = new Dictionary<Message, Action<Frame>>
            {
                {Message.Notify, receiveHeartbeat}     
            };
        }
        public  override void ReceiveFrame(Frame frame)
        {
            switch (frame.messageType)
            {
                case MessageType.Service:
                    registerSubscriber(frame);
                    break;
                case MessageType.ACK:
                    finishSubscription(frame);
                    break;
                default:
                    ActionsOnRecive[frame.message](frame);
                    break;
            }
        }
        private void  receiveHeartbeat(Frame frame)
        {
                Debug.Log("Received heartbeat from subscriber");
        }
        private void registerSubscriber(Frame frame)
        {
            software.radio.AddListener(frame.srcMac);
            identifyMe(frame);
        }
        
        private void finishSubscription(Frame frame)
        {
            Debug.Log("Subscription suceed !");
            
        }
        private void identifyMe(Frame frame)
        {
            Frame identifyMe = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.NoMatter,
                    MessageType.ACK,
                    Message.Subscribe,
                    destMac: frame.srcMac
                );
            software.radio.SendFrame(identifyMe);
        }
    }


}
