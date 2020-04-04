using Simulation.Common;
using Simulation.Utils;
using Simulation.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simulation.UI
{
    public class BuildingsEditor : MonoBehaviour
    {

        public GameObject BuildingButtonTemplate;
        public GameObject MaterialDropdownTemplate;
        [SerializeField]
        private InputField BuildingName;
        private int editing;

        private List<Building> buildings;
        private ScrollViewManager<BuildingButton> building_buttons;
        private ScrollViewManager<MaterialDropDown> material_dropdowns;

        [SerializeField]
        private Mesh ExampleMesh;
        [SerializeField]
        private MeshFilter testFilter;
        [SerializeField]
        private MeshRenderer testRenderer;
        
        public  GameObject testGameObject;

        public void AddNewBuilding()
        {
            SlotContainer slotContainer = new SlotContainer();
            var tmp = new List<Mesh>();
            tmp.Add(ExampleMesh);
            buildings.Add(new Building("New Building",slotContainer, tmp));
            Refresh();
        }
        public void SaveBuilding()
        {
            buildings[editing].Name = BuildingName.text;
            var container = new SlotContainer();
            foreach (var mat in material_dropdowns.elements)
            {   var drop = mat.GetComponent<MaterialDropDown>();
                int amount;
                if (!drop.GetAmount(out amount))
                    return;
                container.AddSlot(BuildingMaterial.existingMaterials[drop.GetMaterialName()], amount);
            }
            buildings[editing].SetContainer(container);
            FileManager.SaveBuildings(buildings);

            Refresh();


        }
       
        public void AddMaterialClick()
        {
            var all = new List<string>(BuildingMaterial.existingMaterials.Keys);

            AddMaterial(all, 0, 0);
        }
        public MaterialDropDown AddMaterial(List<string> options,int amount,int index)
        { 
            var dropdown = material_dropdowns.GenerateList(MaterialDropdownTemplate);
            dropdown.Init(options, amount, index);
            return dropdown;
        }
        public  void OpenEditor()
        {
            this.gameObject.SetActive(true);
            FileManager.ReadMaterials();
            buildings = FileManager.ReadBuildings();
            building_buttons = new ScrollViewManager<BuildingButton>();
            material_dropdowns = new ScrollViewManager<MaterialDropDown>();
            this.testGameObject.SetActive(true);
            Refresh();
            
            
        }
        private void AddBuildings()
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                var building_button = building_buttons.GenerateList(BuildingButtonTemplate);
                building_button.init(buildings[i].Name, this, buildings[i], i);
            }
        }
        void Refresh()
        {
            building_buttons.ClearList();
            material_dropdowns.ClearList();
            AddBuildings();


        }
        public void OpenBuildingMaterials(Building building, int id)
        {

            Refresh();
            BuildingName.text = building.Name;
            var all = new List<string>(BuildingMaterial.existingMaterials.Keys);
            foreach (var item in building.GetFull())
            {
                AddMaterial(all, item.Value, all.IndexOf(item.Key.Type));
            }
            testFilter.mesh = building.GetPreview();
            editing = id;

        }
    }
}