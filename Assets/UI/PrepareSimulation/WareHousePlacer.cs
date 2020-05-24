using Simulation.Common;
using Simulation.Utils;
using Simulation.World;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Simulation.UI
{


    public class WareHousePlacer : MonoBehaviour
    {

        public List<Building> placedBuildings;
        public static List<Warehouse> spawned_warehouses;

        [SerializeField]
        private GameObject material_amount_example;


        [SerializeField]
        private GameObject material_need_example;


        [SerializeField]
        private Transform Landscape;

        [SerializeField]
        private Warehouse warehouse_prefab;
        [SerializeField]
        private Button nextButton;


        private Placer placer;
        private ScrollViewManager<MaterialAmount> materials_in_warehouse;

        private ScrollViewManager<MaterialAmount> needed;
        private ConcurrentDictionary<BuildingMaterial, int> MaterialsNeeded;
        private Warehouse current_warehouse;
        private Camera cam;
   

        void Start()
        {
            placedBuildings = BuildingPlacer.placedBuildings;
            spawned_warehouses = new List<Warehouse>();

            materials_in_warehouse = new ScrollViewManager<MaterialAmount>();
            needed = new ScrollViewManager<MaterialAmount>();

            placer = gameObject.AddComponent<Placer>();
            placer.Init(onPlaced);
            MaterialsNeeded = new ConcurrentDictionary<BuildingMaterial, int>();
            cam = Camera.main;
            foreach (var building in placedBuildings)
            {

                foreach (var item in FileManager.ReadBuildings())
                {
                    if (item.Name==building.Name)
                       building.Init(item.Name,item.GetSlotContainer(),item.GetFrames());
                }
                foreach (var material in building.GetFull())
                {
                   if (MaterialsNeeded.ContainsKey(material.Key))                    
                            MaterialsNeeded[material.Key] += material.Value;
                   else           
                        MaterialsNeeded.TryAdd(material.Key, material.Value);   
                }
            }

            foreach (var item in MaterialsNeeded)
            {
        
                var amount_button = needed.AddElement(material_need_example);
                amount_button.Init(item.Key, item.Value);
            }
        }
       

        private void AddButtons(Warehouse warehouse)
        {      
            if (warehouse.container==null ||warehouse.container.GetContent().IsEmpty)
            {
                SlotContainer new_container = new SlotContainer(MaterialsNeeded);
                warehouse.container = new_container;
            }

            materials_in_warehouse.ClearList();
            foreach (var material in warehouse.container.GetContent())
            {
                var amount_button= materials_in_warehouse.AddElement(material_amount_example);
                amount_button.Init(material.Key,material.Value);
                
            }      
        }
        public void AddNewWarehouse()
        {
            var ware_house_instance = Instantiate(warehouse_prefab.gameObject, Landscape);
            ware_house_instance.GetComponent<Warehouse>().SetPreview(true);
            placer.ChangeObject(ware_house_instance);
            nextButton.interactable = true;
            
        }
        private void onPlaced(GameObject instantiated)
        {
            var warehouse = instantiated.GetComponent<Warehouse>();
            spawned_warehouses.Add(warehouse);
            ChangeCurrentWarehouse(warehouse);
           
        }
        public bool NextStage()
        {
            ChangeCurrentWarehouse(null);
            if (spawned_warehouses.Count>=1)
            {
                var in_warehouses = new ConcurrentDictionary<BuildingMaterial, int>();
                foreach (var item in spawned_warehouses[0].container.GetContent())
                    in_warehouses.TryAdd(item.Key, 0);
                

                foreach (var item in spawned_warehouses)
                    foreach (var content in item.container.GetContent())
                        in_warehouses.AddOrUpdate(content.Key, content.Value, (key, old_value) => old_value + content.Value);         
                
                foreach (var item in in_warehouses)          
                    if (MaterialsNeeded[item.Key] > item.Value)
                        return false;
                return true;

            }
            return false;
        }
        public List<Warehouse> GetWarehouses()
        {
            return spawned_warehouses;
        }
        private void ChangeCurrentWarehouse(Warehouse warehouse)
        {

            if (current_warehouse!=null)
            {
                var new_container = new ConcurrentDictionary<BuildingMaterial,int>();

                foreach (var item in materials_in_warehouse.elements)
                {
                    new_container.TryAdd(item.assigned_material, item.Amount());
                }
              
                current_warehouse.container = new SlotContainer(new_container,true);
               
            }
            if (warehouse!=null)
            {
                AddButtons(warehouse);
                current_warehouse = warehouse;
            }
            
        }




        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var item in spawned_warehouses)
                {
                    item.SetPreview(false);
                }
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var warehouse = hit.collider.GetComponent<Warehouse>();
                    if (warehouse != null) 
                        ChangeCurrentWarehouse(warehouse);
                       
                    
                }
                foreach (var item in spawned_warehouses)
                {
                    item.SetPreview(true);
                }
            }
          
        }
    }
}