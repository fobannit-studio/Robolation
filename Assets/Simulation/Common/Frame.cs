using Simulation.Utils;
using UnityEngine;
namespace Simulation.Common
{
    public struct Frame
    {
        // Check Utils.Enumerations for enum meaning
        public int srcMac, destMac;
        public (float, float, float) payload;
        public TransmissionType transmissionType;
        public DestinationRole destinationRole;
        public MessageType messageType;
        public Message message;

        public Frame(TransmissionType transmissionType,
                     DestinationRole destinationRole,
                     MessageType messageType,
                     Message message,
                     int srcMac,
                     int destMac,
                     (float, float, float) payload)
        {

            this.transmissionType = transmissionType;
            this.destinationRole = destinationRole;
            this.messageType = messageType;
            this.message = message;
            this.srcMac = srcMac;
            this.destMac = destMac;
            this.payload = payload;
        }

        public Frame(TransmissionType transmissionType,
                     DestinationRole destinationRole,
                     MessageType messageType,
                     Message message,
                     int srcMac,
                     (float, float, float) payload)
                     //  -1 passed as dest MAC because MAC assigment in ether starts from 0, so -1 is impossible value
                     : this(transmissionType, destinationRole, messageType, message, srcMac, -1, payload)
        { }
        public Frame(TransmissionType transmissionType,
                    DestinationRole destinationRole,
                    MessageType messageType,
                    Message message,
                    int srcMac,
                    int destMac,
                    (float first, float second) payload)
                    : this(transmissionType, destinationRole, messageType, message, srcMac, destMac, (payload.first, payload.second, 0))
        { }
        public Frame(TransmissionType transmissionType,
                DestinationRole destinationRole,
                MessageType messageType,
                Message message,
                int srcMac,
                (float first, float second) payload)
                : this(transmissionType, destinationRole, messageType, message, srcMac, -1, (payload.first, payload.second, 0))
        { }
        public Frame(TransmissionType transmissionType,
                    DestinationRole destinationRole,
                    MessageType messageType,
                    Message message,
                    int srcMac,
                    int destMac,
                    float payload)
                    : this(transmissionType, destinationRole, messageType, message, srcMac, destMac, (payload, 0, 0))
        { }
        public Frame(TransmissionType transmissionType,
            DestinationRole destinationRole,
            MessageType messageType,
            Message message,
            int srcMac,
            float payload)
            : this(transmissionType, destinationRole, messageType, message, srcMac, -1, (payload, 0, 0))
        { }


        public override string ToString()
        {
            return $"From {this.srcMac} to {this.destMac}. Payload {this.payload}";
        }
    }
}