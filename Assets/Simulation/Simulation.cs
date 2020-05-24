
using UnityEngine;
using Simulation.Robots;
using Simulation.World;
using System.Collections.Generic;
using Simulation.Software;
using System;
using UnityEngine.Rendering;
using System.Data;

namespace Simulation
{
    public class Simulation : MonoBehaviour


    {


        [SerializeField]
        private Light DirectionalLight;
        [SerializeField]
        private LightingPreset preset;

        [SerializeField, Range(0, 24)]
        private float TimeOfDay;
        [SerializeField]
        private Light NightLight;




        public static List<Robot> Robots { get; private set; } = new List<Robot>();
        public static List<Building> Buildings { get; private set; } = new List<Building>();
        public static List<Building> NotAdministratedBuildings = new List<Building>();
        public static List<Warehouse> Warehouses { get; private set; } = new List<Warehouse>();
        public bool CycleActive { get; private set; }

        public void Init(List<(Type soft, Robot robot)> robots, List<Building> buildings, List<Warehouse> warehouses)
        {
            Medium ether = new Medium();
            Buildings = buildings;
            NotAdministratedBuildings = buildings; 
            Warehouses = warehouses;
            Vector3 offset = new Vector3();
            foreach (var item in robots)
            {
                item.robot.Init(1000, ether);
                var soft = Activator.CreateInstance(item.soft) as RobotOperatingSystem;
                soft.Init(item.robot);
                Robots.Add(item.robot);
                Debug.Log(string.Format("Installed {0} on {1}", item.soft.Name, item.robot.GetType().Name));
                if (soft.GetType() == typeof(TransporterSoftware))
                {
                    Vector3 pos = (soft as TransporterSoftware).FindClosestWarehouse();
                    item.robot.MoveOrder(pos + offset);
                    offset.x += 0.5F;
                }
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
            CycleActive = true;

        
        }
        private void Update()
        {  if (!CycleActive) return;

            if (preset == null) return;

            TimeOfDay += Time.deltaTime/10;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);

          

        }
        void UpdateLighting(float timePercent)
        {
            RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
            if (DirectionalLight!=null)
            {
                DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
                DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            }

            if (timePercent < 0.3 || timePercent > 0.7)
                NightLight.enabled = true;
            else
                NightLight.enabled = false;

        }
        private void OnValidate()
        {
            if (DirectionalLight != null)
                return;
            if (RenderSettings.sun != null)
                DirectionalLight = RenderSettings.sun;
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        DirectionalLight = light;
                        return;
                    }
                      

                    
                }
            }
              

        }
    }
}