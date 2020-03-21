using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using Simulation.Components;
using UnityEngine;
namespace Simulation.Software
{
    abstract class OperatingSystem : ICommunicator
    {
        // Robot to which this role is assigned.
        public readonly Radio radio;
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
        public OperatingSystem(ref Robot robot)
        {
            attributedRobot = robot;
            attributedRobot.radio.software = this;
            radio = attributedRobot.radio;

            foreach (KeyValuePair<Message, FrameAction> action in MyFrameActions)
            {
                // Install each action on this software
                action.Value.installOn(this);
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
        public void HandleFrame(Frame frame)
        {
            if (isForMe(frame))
            {
                Debug.Log($"{this.GetType().Name} recognized itself and start parsing message: {frame}");
                foreach (KeyValuePair<Message, FrameAction> action in MyFrameActions)
                {
                    if(action.Value.isMyFrame(frame))
                        action.Value.Receive(frame);
                }
            }
        }
    }
}