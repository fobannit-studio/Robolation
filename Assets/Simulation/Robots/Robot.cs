using UnityEngine;
using Simulation.World;
using Simulation.Components;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using System.Threading.Tasks;

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
        public float PickupRange=5f;
        public Radio radio;
        // public IReceiver controller;
        [SerializeField]
        protected NavMeshAgent agent;
        protected Container container;
        protected Animator animator;

        public Container MaterialContainer { get => container; }

        protected abstract int cointainer_size { get; }

        // Every robot on creation should register himself 
        // in ether.
       
        public void Init(float radioRange, Medium ether)
        {
            batteryLevel = 1.0F;
            container = new Container(cointainer_size);
            PickupRange = 0.5f;
            animator = GetComponent<Animator>();
            radio = new Radio(radioRange, ether);
        }
        

        virtual public void MoveOrder(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
        
        
        //public bool PickupFromWarehouse(BuildingMaterial material,int count)
        //{
          
        //    var colliders = Physics.OverlapSphere(transform.position, PickupRange);
        //    foreach (var collider in colliders)
        //    {
        //        var warehouse = collider.GetComponent<Warehouse>();
        //        if (warehouse)
        //           return warehouse.container.TransferTo(this.container,material,count);
                
        //    }
        //    return false;
        //}

    

     
    
        public bool PutObject(IContainer container,BuildingMaterial material,int count)
        {

            return this.container.TransferTo(container, material, count);

        }
        public bool PickupObject(IContainer container, BuildingMaterial material,int count)
        {
           return container.TransferTo(this.container, material, count);
        }
       
    }
}