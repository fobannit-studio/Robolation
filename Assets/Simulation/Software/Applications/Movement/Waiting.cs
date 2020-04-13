using Simulation.Common;
using Simulation.Utils;
// 
using UnityEngine;
namespace Simulation.Software
{
    public class Waiting : CommunicationBasedApplicationState
    {
        public Waiting(Application app) : base(app) { }
        public override void Send()
        {
            Debug.Log("Nothing to send!");
        }
        public override void Receive(Frame frame)
        {
            Debug.Log($"Received frame  {frame}. Start moving");
            ((Movement)Application).SetMovingState();
            (float x,float y,float z) = frame.payload;
            AttributedSoftware.attributedRobot.MoveOrder(new Vector3(x,y,z));
            ConfirmReceiving(frame.srcMac);
        
        }
        private void ConfirmReceiving(int destMac)
        {
            Frame response = new Frame(
               TransmissionType.Unicast,
               DestinationRole.Operator,
               MessageType.ACK,
               Message.MoveTo,
               destMac: destMac);
            AttributedSoftware.radio.SendFrame(response);
        }
    }
}