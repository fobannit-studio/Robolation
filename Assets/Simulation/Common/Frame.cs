using System;
using System.Text;
using Simulation.Utils;
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
        public void Deconstruct(out float x, out float y, out float z) 
        => (x,y,z) = (floatPayload[0],floatPayload[1],floatPayload[2]);
        public void Deconstruct(out string text, out float x, out float y, out float z)
        {
            text = textPayload;
            Deconstruct(out x, out y, out z);
        }

    }
    //TODO change naming on upper case
    public struct Frame
    {
        // Check Utils.Enumerations for enum meaning
        public int srcMac, destMac;

        public TransmissionType transmissionType;
        public DestinationRole destinationRole;
        public Type SendingOS;
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
            Type sendingOS = null,
            Payload? payload = null
        )
        {
            this.transmissionType = transmissionType;
            this.destinationRole = destinationRole;
            this.messageType = messageType;
            this.message = message;
            this.srcMac = srcMac;
            SendingOS = sendingOS;
            this.destMac = destMac;
            this.payload = payload ?? new Payload();
        }

        public override string ToString()
        {
            return $"From {this.srcMac} to {this.destMac} {this.destinationRole}. Action: {this.message.ToString("F")}\n Payload: {this.payload.ToString()}.";
        }
    }
}