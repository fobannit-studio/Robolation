using Simulation.Common;
using Simulation.Utils;
using UnityEngine;
namespace Simulation.Roles
{
    abstract class Role
    {
        protected abstract DestinationRole IReceive
        {
            get;
        }
        protected bool isForMe(Frame message)
        {
            return message.destinationRole == IReceive || message.destinationRole is DestinationRole.NoMatter;
        }

        public void ReceiveFrame(Frame message)
        {
            if (isForMe(message))
            {
                Debug.Log($"{this.GetType().Name} received message {message}");
                parseMessageType(message);
            }
        }
        protected void parseMessageType(Frame message)
        {
            if (message.messageType is MessageType.Service)
            {
                handleService(message);
            }
            else if (message.messageType is MessageType.Request)
            {
                handleRequest(message);
            }
        }
        // Implementation defines service messages, that could be received by this Role.
        // After message handling creates a new frame with ACK in case of success or NACK in case of failure.
        // Sends created frame back to author.
        protected abstract void handleService(Frame message);
        // Implementation defines request messages that could be received by this Role.
        // After message receiving creates a new Frame with data.
        // Sends created frame back to author.
        protected abstract void handleRequest(Frame message);
    }
}