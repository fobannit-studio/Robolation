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
        protected float batteryLevel;
        protected float durability;
        protected int workingTime;
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

        }

        // Method that each robot perform, but which invoked by his roles.
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