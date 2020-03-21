using System;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using Simulation.World;
using Simulation.Software;
using Simulation.Utils;
using Simulation.Components;
using UnityEngine.AI;
namespace Simulation.Robots
{
    // Physical description of robot, that have params
    // such as workingArea, battery, etc.
    public abstract class Robot:MonoBehaviour
    {
        // protected float radioRange;
        // Describes how many subscribers robot's radio can handle.
        // public int maxSubscribersNumber; 
        public Vector3 Position
        {
            get;
        }
        protected float batteryLevel;
        protected float durability;
        protected int workingTime;
        public Radio radio;
        // public IReceiver controller;

        protected NavMeshAgent agent;

        // Every robot on creation should register himself 
        // in ether.
        public Robot(Vector3 positionInWorld, float radioRange, ref Medium ether)
        {
            batteryLevel = 1.0F;
            Position = positionInWorld;    
            radio = new Radio(radioRange, 1, ref ether); 

        }
        virtual public void MoveOrder(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
        public virtual void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
}