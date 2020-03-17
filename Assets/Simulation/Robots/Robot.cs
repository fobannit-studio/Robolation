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
        protected Vector2 position;
        protected readonly Action<int, Frame> SendFrame;
        // Array of subscribed MAC-addresses
        protected int macAddress;
        protected int battery;
        protected float durability;
        protected int workingTime;
        public List<int> Subscribers = new List<int>();

        protected abstract Role Role{
            get;
        }
        // Every robot on creation should register himself 
        // in ether.
        public Robot(Vector2 position,ref Medium ether)
        {
            this.position = position; 
            macAddress = ether.RegisterRadio(this.Role.ReceiveFrame, () => this.position);
            SendFrame = ether.Transmit;
        }
                
        public void NotifySubscribers(Frame message)
        {
            foreach (int subscriber in Subscribers)
            {
                SendFrame(subscriber, message);   
            }
        }
    }
}