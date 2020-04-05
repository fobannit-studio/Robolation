using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class ListeningForSubscriptions : CommunicationBasedApplicationState
    {
        public ListeningForSubscriptions(Application app) : base(app) { }
        public override void Send()
        {
        }
        public override void Receive(Frame frame)
        {
            /// tmp
            if (frame.messageType is MessageType.Service)
            {
                radio.AddListener(frame.srcMac);
                Frame identifyMe = new Frame(
                        TransmissionType.Unicast,
                        DestinationRole.NoMatter,
                        MessageType.ACK,
                        Message.Subscribe,
                        destMac: frame.srcMac
                    );
                radio.SendFrame(frame);

            }
            else
            {
                Debug.Log("Received heartbeat from subscriber");
            }
        }
    }
}