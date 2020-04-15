using Simulation.Common;
using Simulation.Utils;
using System.Collections.Generic;
using UnityEngine;
namespace Simulation.Software
{
    public class Subscribing : CommunicationBasedApplicationState
    {
        private Dictionary<int, SubscriberTracking> SubscriberTrackingApps = new Dictionary<int, SubscriberTracking>(); 
        public Subscribing(Application app) : base(app) { }
        public override void Send()
        { }
        public override void Receive(Frame frame)
        {
            if(!SubscriberTrackingApps.ContainsKey(frame.srcMac))
            {
                createNewReceivingApplication(frame);
            }
            else
            {
                SubscriberTrackingApps[frame.srcMac].ReceiveFrame(frame);
            }
        }
        private void createNewReceivingApplication(Frame frame)
        {
            var newApp = AttributedSoftware.GameObject.AddComponent<SubscriberTracking>();
            newApp.installOn(AttributedSoftware);
            SubscriberTrackingApps.Add(frame.srcMac, newApp);
            radio.AddListener(frame.srcMac);
            var identifyMe = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.NoMatter,
                    MessageType.ACK,
                    Message.Subscribe,
                    destMac: frame.srcMac
                );
            radio.SendFrame(identifyMe);
        }
    }
}