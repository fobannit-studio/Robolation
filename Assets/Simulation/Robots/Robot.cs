using System;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using Simulation.World;
using Simulation.Roles;
using Simulation.Utils;
using Simulation.Components;
namespace Simulation.Robots
{
    // Physical description of robot, that have params
    // such as workingArea, battery, etc.
    abstract class Robot
    {
        // protected float radioRange;
        // Describes how many subscribers robot's radio can handle.
        // public int maxSubscribersNumber; 
        public Vector2 Position
        {
            get;
        }
        // public int macAddress;
        protected float batteryLevel;
        protected float durability;
        protected int workingTime;
        // Array of subscribed MAC-addresses
        // public List<int> Subscribers = new List<int>();

        public Radio radio;
        public IReceiver controller;
        // Every robot on creation should register himself 
        // in ether.
        public Robot(Vector2 positionInWorld, float radioRange, ref Medium ether)
        {
            batteryLevel = 1.0F;
            Position = positionInWorld;
            // Every robot by default could listen only 
            // for one other robot 
            radio = new Radio(radioRange, 1, ref ether); 

            // Move to radio class definition.
            // maxSubscribersNumber = 1;
            // this.radioRange = radioRange;
            // macAddress = ether.RegisterRadio(ReceiveFrame);
            // SendFrame = ether.Transmit;
        }
        
        // Move to radio 
        // Answer the question if this robot's radio can receive a 
        // message at this moment
        // protected bool couldReceive()
        // {
        //     return !System.Object.ReferenceEquals(role,null) && batteryLevel > 0.1; 
        // }
        // // In this method will be handled such conditions as robot battery level, 
        // // radio condition, etc/
        // public void HandleFrame(Frame frame)
        // {
        //     if(couldReceive() && frame.srcMac != this.macAddress)
        //     {
        //         Debug.Log($"{this.GetType().Name} radio received Frame.");
        //         role.ReceiveFrame(frame);
        //     }
        // }
        // public void NotifySubscribers(Frame message)
        // {
        //     foreach (int subscriber in Subscribers)
        //     {
        //         SendFrame(message);   
        //     }
        // }
        // public bool addSubscriber(int robotId, float x, float y)
        // {
        //     if(Subscribers.Count < maxSubscribersNumber && anotherRadioInRange(x,y))
        //     {
        //         Subscribers.Add(robotId);
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }
        // public bool anotherRadioInRange(float otherX, float otherY)
        // {
        //     Vector2 otherPosition = new Vector2(otherX, otherY);
        //     return Vector2.Distance(position, otherPosition) < radioRange;
        // }

        // Method that robot, perform, but which invoked by his roles.
        public void FindOperator()
        {
            Frame findOperatorFrame = new Frame(
                TransmissionType.Broadcast,
                DestinationRole.Operator,
                MessageType.Service,
                Message.Subscribe,
                (Position.x, Position.y) 
            );
            radio.SendFrame(findOperatorFrame);
        }
    }
}