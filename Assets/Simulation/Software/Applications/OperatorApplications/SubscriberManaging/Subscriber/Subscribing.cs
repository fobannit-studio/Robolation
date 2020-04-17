using Simulation.Common;
using Simulation.Utils;
using System.Collections.Generic;
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
            Application.Radio.AddListener(frame.srcMac);
            Application.CreateAppBasedOnFrame(frame, SubscriberTrackingApps);
            var (x,y,z) = frame.payload;
            AttributedSoftware.RoutingTable.Add((frame.SendingOS, frame.srcMac), (x,y,z));
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