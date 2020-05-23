
using UnityEngine;
using Simulation.Robots;
using Simulation.World;
using System.Collections.Generic;
using Simulation.Software;
using System;
using UnityEngine.Rendering;

namespace Simulation
{
    public class Simulation : MonoBehaviour
    {
        public static List<Robot> Robots { get; private set; } = new List<Robot>();
        public static List<Building> Buildings { get; private set; } = new List<Building>();
        public static List<Building> NotAdministratedBuildings = new List<Building>();
        public static List<Warehouse> Warehouses { get; private set; } = new List<Warehouse>();
        public void Init(List<(Type soft, Robot robot)> robots, List<Building> buildings, List<Warehouse> warehouses)
        {
            Medium ether = new Medium();
            Buildings = buildings;
            NotAdministratedBuildings = buildings; 
            Warehouses = warehouses;
            foreach (var item in robots)
            {
                item.robot.Init(1000, ether);
                var soft = Activator.CreateInstance(item.soft) as RobotOperatingSystem;
                soft.Init(item.robot);
                Robots.Add(item.robot);
                Debug.Log(string.Format("Installed {0} on {1}", item.soft.Name, item.robot.GetType().Name));
            }

            foreach (var warehouse in warehouses)
            {
                warehouse.SetPreview(false);
            }
            foreach (var building in buildings)
            {
                building.SetFrame(0);
                building.SetActual();
            }


            Debug.Log("Done ");
        }
    }
}