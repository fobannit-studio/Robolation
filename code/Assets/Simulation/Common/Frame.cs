using System;
using System.Text;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Common
{
    public struct Payload
    {
        public string textPayload;
        public float[] floatPayload;
        
        public Payload(float[] floatPayload = null, string textPayload = "")
        {
            this.floatPayload = floatPayload ?? new float[] { };
            this.textPayload = textPayload;
        }
        public Payload(string textPayload, float[] floatPayload = null) : this(floatPayload, textPayload)
        { }
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(floatPayload != null){
                foreach (float value in floatPayload)
                    sb.Append($"{value}, ");
            }
            sb.Append(textPayload);
            return sb.ToString();
        }

    }
    public struct Frame
    {
        // Check Utils.Enumerations for enum meaning
        public int srcMac, destMac;

        public TransmissionType transmissionType;
        public DestinationRole destinationRole;
        public MessageType messageType;
        public Message message;
        public Payload payload;


        public Frame(
            TransmissionType transmissionType,
            DestinationRole destinationRole,
            MessageType messageType,
            Message message,
            // in case if it is a frame for broadcast between subscribers
            int destMac = -1,
            int srcMac = -1,
            Payload? payload = null
        )
        {
            this.transmissionType = transmissionType;
            this.destinationRole = destinationRole;
            this.messageType = messageType;
            this.message = message;
            this.srcMac = srcMac;
            this.destMac = destMac;
            this.payload = payload ?? new Payload();
        }

        public override string ToString()
        {
            return $"From {this.srcMac} to {this.destMac}. Action: {this.message.ToString("F")}\n Payload: {this.payload.ToString()}";
        }
    }
}