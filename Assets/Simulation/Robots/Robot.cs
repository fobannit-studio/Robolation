using UnityEngine;
using Simulation.World;
using Simulation.Components;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using System.Threading.Tasks;
using System;
using System.Linq;
using Simulation.Software;

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
        public float PickupRange;
        public Radio radio;

        // public IReceiver controller;
        [SerializeField]
        protected NavMeshAgent agent;
        protected Container container;
        protected Animator animator;


        public abstract int BuildIterations { get; }
        public int IterationsPassed;


        public Container MaterialContainer { get => container; }

        protected abstract int cointainer_size { get; }

        // Every robot on creation should register himself 
        // in ether.
       
        public void Init(float radioRange, Medium ether)
        {
            batteryLevel = 1.0F;
            container = new Container(cointainer_size);
            PickupRange = 100;
            animator = GetComponent<Animator>();
            radio = new Radio(radioRange, ether);
        }
        

        virtual public void MoveOrder(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
        private void Update()
        {

        }


        public T NearestToPickup<T>() where T : MonoBehaviour
        {
            Debug.Log(this.transform.position);
            var colliders = FindObjectsOfType<T>();
            var possible = colliders.Where(x => (x.transform.position - this.transform.position).magnitude <= PickupRange && this!=x)
                .OrderBy(x => (transform.position - x.transform.position).magnitude).ToList();
            if (possible.Count == 0)
                return null;
            else
            {
                return possible[0];
            }
        }


        public bool PutObject(IContainer container,BuildingMaterial material,int count)
        {

            return this.container.TryTransferTo(container, material, count)!=0;

        }
        public bool PickupObject(IContainer container, BuildingMaterial material,int count)
        {
           return container.TryTransferTo(this.container, material, count)!=0;
        }
    

    }
}