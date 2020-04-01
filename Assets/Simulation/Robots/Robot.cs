using UnityEngine;
using Simulation.World;
using Simulation.Components;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

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
            get => transform.position;
        }
        protected float batteryLevel;
        protected float durability;
        protected int workingTime;
        public Radio radio;
        // public IReceiver controller;
        [SerializeField]
        protected NavMeshAgent agent;


        // Every robot on creation should register himself 
        // in ether.
       
        public void Init(float radioRange, ref Medium ether)
        {
            batteryLevel = 1.0F;
            radio = new Radio(radioRange, ref ether);
        }

        virtual public void MoveOrder(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
       
    }
}