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
        public float PickupRange { get; protected set; }
        public Radio radio;
        // public IReceiver controller;
        [SerializeField]
        protected NavMeshAgent agent;
        protected Container container;
        protected Animator animator;



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
        

        async public Task<bool> PickupObject(Warehouse warehouse,BuildingMaterial material)
        {
            bool result = false;
            if ((warehouse.transform.position - this.transform.position).magnitude < PickupRange)
            {
                
                animator.SetBool("opening", true);
                await Task.Delay(1);
                if (container.CanPut(material, 1))
                {
                    if (warehouse.container.CanTake(material, 1))
                    {
                        warehouse.container.Take(material, 1);
                        container.Put(material, 1);
                        result = true;
                    }
                    else result = false;
                }
                else result = false;


                animator.SetBool("opening", false);
                await Task.Delay(1);
                return result;
              
            }
            else
                throw new WarehouseNotInRangeException();
        }
       
    }
}