using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using UnityEngine;
namespace Simulation.Software
{
    abstract class Software : ICommunicator
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
        protected abstract Dictionary<Message, FrameAction> MyFrameActions
        {
            get;
        }
        public Software(ref Robot robot)
        {
            attributedRobot = robot;
            attributedRobot.radio.software = this;
            foreach (KeyValuePair<Message, FrameAction> action in MyFrameActions)
            {
                action.Value.assignRadio(attributedRobot.radio);
            }
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
        public void HandleFrame(Frame frame)
        {
            if (isForMe(frame))
            {
                Debug.Log($"{this.GetType().Name} recognized itself and start parsing message: {message}");
                foreach (KeyValuePair<Message, FrameAction> action in MyFrameActions)
                {
                    if(action.Value.isMyFrame(frame))
                        action.Value.Receive(frame);
                }
                // parseMessageType(message);
            }
        }
    //     protected void parseMessageType(Frame message)
    //     {
    //         if (message.messageType is MessageType.Service)
    //         {
    //             handleService(message);
    //         }
    //         else if (message.messageType is MessageType.Request)
    //         {
    //             handleRequest(message);
    //         }
    //         else if (message.messageType is MessageType.ACK)
    //         {
    //             handleAcknowledge(message);
    //         }
    //     }
    //     // Implementation defines service messages, that could be received by this Role.
    //     // After message handling creates a new frame with ACK in case of success or NACK in case of failure.
    //     // Sends created frame back to author.
    //     protected abstract void handleService(Frame message);
    //     // Implementation defines request messages that could be received by this Role.
    //     // After message receiving creates a new Frame with data.
    //     // Sends created frame back to author.
    //     protected abstract void handleRequest(Frame message);
    //     // Implementation defines how robot should react on received ack Frame
    //     protected abstract void handleAcknowledge(Frame message);
    // }
}