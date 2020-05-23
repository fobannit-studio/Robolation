using System;
using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using Simulation.Components;
using UnityEngine;
using System.Collections.ObjectModel;
using Simulation.World;

namespace Simulation.Software
{
    public abstract class RobotOperatingSystem : ICommunicator
    {
        /// <summary>
        /// Contain Types of subscribed operating systems, and mac addresses assigned to them
        /// </summary>
        public Dictionary<(Type type, int mac), (float x, float y, float z)> RoutingTable { get; } = new Dictionary<(Type type, int mac), (float x, float y, float z)>();
        public Radio Radio;
        public Robot attributedRobot;
        protected int operatorMac;
        // TODO Check if could be removed
        public int OperatorMac
        {
            set => operatorMac = value;
            get => operatorMac;
        }
        public GameObject GameObject {get => attributedRobot.gameObject;}
        protected abstract DestinationRole IReceive { get; }
        public Vector3 Position => attributedRobot.transform.position;
        public ReadOnlyCollection<Application> ReqiuredSoft => requiredSoft.AsReadOnly();
        protected List<Application> requiredSoft;
        protected abstract void LoadSoft();
        public bool PickUpFromWarehouse(string material, int amount)
        {
            Debug.Log($"Picking {material} in amount {amount} from warehoues");
            BuildingMaterial buildMaterial = BuildingMaterial.existingMaterials[material];
            var warehouse = attributedRobot.NearestToPickup<Warehouse>();
            if (warehouse)
            {

                int result = warehouse.container.TryTransferTo(attributedRobot.MaterialContainer, buildMaterial, amount);
                return result != 0;
            }
                
            else return false;

        }
        public void Init(Robot robot)
        {
            attributedRobot = robot;
            attributedRobot.radio.software = this;
            Radio = attributedRobot.radio;
            operatorMac = -1;
            LoadSoft();
            InstallSoft();
        }
        protected void InstallSoft()
        {
            foreach (var application in ReqiuredSoft)
            {
                application.installOn(this);
            }
        }
        protected bool isForMe(Frame message)
        {
            return message.destinationRole == IReceive || message.destinationRole is DestinationRole.NoMatter;
        }
        public void HandleFrame(Frame frame)
        {
            if (isForMe(frame))
            {
                //Debug.Log($"{this.GetType().Name} recognized itself and start parsing message: {frame}");
                foreach (var application in ReqiuredSoft)
                {
                    application.ReceiveFrame(frame);
                }
            }
        }
    }
}
