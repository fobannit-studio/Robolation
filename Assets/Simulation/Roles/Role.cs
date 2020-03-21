using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    abstract class Software: IReceiver
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

        public Software(ref Robot robot)
        {
            attributedRobot = robot;
            attributedRobot.radio.software = this;
        }
        protected abstract DestinationRole IReceive
        {
            get;
        }
        protected bool isForMe(Frame message)
        {
            return message.destinationRole == IReceive || message.destinationRole is DestinationRole.NoMatter;
        }
        /// <summary>
        /// Handle received frame.
        /// </summary>
        /// <param name="message">Receivde frame.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// void.
        /// </returns>
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
        // Method that each robot perform, but which invoked by his roles.
        protected void FindOperator()
        {
            Frame findOperatorFrame = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Operator,
                MessageType.Service,
                Message.Subscribe,
                (0, 0, 0) 
            );
            attributedRobot.radio.SendFrame(findOperatorFrame);
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