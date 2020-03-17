using System;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using Simulation.World;
using Simulation.Roles;
using Simulation.Utils;
namespace Simulation.Robots
{
    abstract class Robot
    {
        protected float radioRange;
        // Describes how many subscribers robot's radio can handle.
        public int maxSubscribersNumber; 
        protected Vector2 position;
        protected readonly Action<Frame> SendFrame;
        public int macAddress;
        protected float batteryLevel;
        protected float durability;
        protected int workingTime;
        // Array of subscribed MAC-addresses
        public List<int> Subscribers = new List<int>();

        public abstract Role role{
            get; 
        }
        // Every robot on creation should register himself 
        // in ether.
        public Robot(Vector2 positionInWorld, float radioRange, ref Medium ether)
        {
            this.radioRange = radioRange;
            maxSubscribersNumber = 1;
            batteryLevel = 1.0F;
            position = positionInWorld; 
            macAddress = ether.RegisterRadio(ReceiveFrame);
            SendFrame = ether.Transmit;
        }
        
        // In this method will be handled such conditions as robot battery level, 
        // radio condition, etc/
        public void ReceiveFrame(Frame frame)
        {
            if(batteryLevel > 0.1 && frame.srcMac != macAddress)
            {
                Debug.Log($"{this.GetType().Name} radio received Frame.");
                role.ReceiveFrame(frame);
            }
        }
        public void NotifySubscribers(Frame message)
        {
            foreach (int subscriber in Subscribers)
            {
                SendFrame(message);   
            }
        }
        public bool addSubscriber(int robotId, float x, float y)
        {
            if(Subscribers.Count < maxSubscribersNumber && anotherRadioInRange(x,y))
            {
                Subscribers.Add(robotId);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool anotherRadioInRange(float otherX, float otherY)
        {
            Vector2 otherPosition = new Vector2(otherX, otherY);
            return Vector2.Distance(position, otherPosition) < radioRange;
        }

        // Method that robot, perform, but which invoked by his roles.
        public void FindOperator()
        {
            Frame findOperatorFrame = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Operator,
                MessageType.Service,
                Message.Subscribe,
                this.macAddress,
                (position.x, position.y) 
            );
            SendFrame(findOperatorFrame);
        }
    }
}