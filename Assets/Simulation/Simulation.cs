
using UnityEngine;
using Simulation.Robots;
using Simulation.World;
using System.Collections.Generic;
using Simulation.Software;
using System;
using UnityEngine.Rendering;
using System.Data;
using System.Linq;
using Simulation.UI;

namespace Simulation
{
    public struct SimulationParameters
    {
        public List<(Type soft, Robot robot)> robots;
        public List<Building> buildings;
        public List<Warehouse> warehouses;
    }
    public class Simulation : MonoBehaviour


    {

        public float PassedTime { get; private set; }
        [SerializeField]
        private Light DirectionalLight;
        [SerializeField]
        private LightingPreset preset;
        [SerializeField, Range(0, 24)]
        private float TimeOfDay;
        [SerializeField]
        private Light NightLight;
        [SerializeField]
        private Summary summary;

        public static List<Robot> Robots { get; private set; } = new List<Robot>();
        public static List<Building> Buildings { get; private set; } = new List<Building>();
        public static List<Building> NotAdministratedBuildings = new List<Building>();
        public static List<Warehouse> Warehouses { get; private set; } = new List<Warehouse>();
        public bool CycleActive { get; private set; }



        public void Init(SimulationParameters parameters)
        {
            Medium ether = new Medium();
            PassedTime = 0;
            TimeOfDay = 0;

            Buildings = parameters.buildings;
            NotAdministratedBuildings = parameters.buildings; 
            Warehouses = parameters.warehouses;
            Vector3 offset = new Vector3();
            foreach (var item in parameters.robots)
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

            foreach (var warehouse in parameters.warehouses)
            {
                warehouse.SetPreview(false);
                warehouse.FillWithMaterials();

            }
            foreach (var building in parameters.buildings)
            {
                building.ClearMaterials();
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
            PassedTime += Time.deltaTime;
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
        public void BuildingFinished()
        {
            Debug.Log("Fiinished");
            Debug.Log($"Buildins built {Buildings.Where(x => x.isFinished).Count()}");
            Debug.Log($"Total count {Buildings.Count}");

            if (Buildings.Where(x=>x.isFinished).Count()==Buildings.Count)
            {
                CycleActive = false;
                summary.Finished(this);
            }
        }
    }
}