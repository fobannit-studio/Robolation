using Simulation.Common;
using Simulation.Software;
using System;
using UnityEngine;
using Simulation.Utils;
namespace Simulation.Software
{
    class WaitingForTransportOrders : CommunicationBasedApplicationState
    {
        public WaitingForTransportOrders(Application app): base(app) { }
        public override void Receive(Frame frame)
        {
            var confirmationOfAvailibility = new Frame(
                TransmissionType.Unicast,
                DestinationRole.Operator,
                MessageType.ACK,
                Message.isFree,
                destMac: frame.srcMac
                );
            (Application as MaterialTransfering).StartWaitingForMaterialInfo();
            Debug.Log($"Transporter received {frame}");
            AttributedSoftware.Radio.SendFrame(confirmationOfAvailibility);
        }
    }
}
 