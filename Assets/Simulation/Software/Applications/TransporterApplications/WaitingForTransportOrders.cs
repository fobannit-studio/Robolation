using Simulation.Common;
using Simulation.Software;
using System;
using UnityEngine;
using Simulation.Utils;
namespace Simulation.Software
{
    class WaitingForTransportOrders : CommunicationBasedApplicationState
    {
        // Add courutine that will set this to false after some time
        private bool isWaitingForConfirm = false;
        private int waiting = 0;
        public WaitingForTransportOrders(Application app): base(app) { }
        public override void Receive(Frame frame)
        {
            if (frame.message is Message.isFree && !isWaitingForConfirm )
            {
                var confirmationOfAvailibility = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Operator,
                    MessageType.ACK,
                    Message.isFree,
                    destMac: frame.srcMac
                    );
                isWaitingForConfirm = true;
                AttributedSoftware.Radio.SendFrame(confirmationOfAvailibility);
            }
            else if (frame.message is Message.StartTransporting && frame.messageType is MessageType.Request && isWaitingForConfirm) 
            {
                var confirmStart = new Frame(
                    TransmissionType.Unicast,
                    DestinationRole.Operator,
                    MessageType.ACK,
                    Message.StartTransporting,
                    destMac: frame.srcMac);
                (Application as MaterialTransfering).StartWaitingForMaterialInfo();
                AttributedSoftware.Radio.SendFrame(confirmStart);
            }
        }
        public override void DoAction()
        {
            if (waiting > 1) 
            {
                isWaitingForConfirm = false;
                waiting = 0;
            }
            waiting += 1;
            
        }
    }
}
 