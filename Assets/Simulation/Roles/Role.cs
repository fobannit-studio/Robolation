using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Roles
{
    abstract class Role: IReceiver
    {
        // Robot to which this role is assigned.
        protected Robot attributedRobot;
        public Vector2 Position
        {
            get
            {
                return attributedRobot.Position;
            }
        }

        public Role(ref Robot robot)
        {
            attributedRobot = robot;
            attributedRobot.radio.controller = this;
        }
        protected abstract DestinationRole IReceive
        {
            get;
        }
        protected bool isForMe(Frame message)
        {
            return message.destinationRole == IReceive || message.destinationRole is DestinationRole.NoMatter;
        }

        public void HandleFrame(Frame message)
        {
            if (isForMe(message))
            {
                Debug.Log($"{this.GetType().Name} recognized itself and start parsing message: {message}");
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