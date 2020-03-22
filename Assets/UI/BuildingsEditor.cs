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
        private List<GameObject> buttons;

        private List<GameObject> attributes;

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
            foreach (var mat in attributes)
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
          
            GameObject button = Instantiate(MaterialDropdownTemplate) as GameObject;
            button.SetActive(true);
            var tmp = button.GetComponent<MaterialDropDown>();
            tmp.Init(options, amount, index);
            button.transform.SetParent(MaterialDropdownTemplate.transform.parent, false);
            attributes.Add(button);

            return tmp;
        }
        public  void OpenEditor()
        {
            this.gameObject.SetActive(true);
            FileManager.ReadMaterials();
            buildings = FileManager.ReadBuildings();
            buttons = new List<GameObject>();
            attributes = new List<GameObject>();
            this.testGameObject.SetActive(true);
            Refresh();
            
            
        }
        private void AddBuildings()
        {
            for (int i = 0; i < buildings.Count; i++)
            {

                GameObject button = Instantiate(BuildingButtonTemplate) as GameObject;
                button.SetActive(true);
                var tmp = button.GetComponent<BuildingButton>();
                tmp.init(buildings[i].Name, this, buildings[i], i);
                button.transform.SetParent(BuildingButtonTemplate.transform.parent, false);
                buttons.Add(button);
            }
        }
        void Refresh()
        {
            foreach (var item in buttons)
            {
                Destroy(item);
            }
            foreach (var item in attributes)
            {
                Destroy(item);
            }
            buttons = new List<GameObject>();
        
            attributes = new List<GameObject>();
            AddBuildings();


        }
        public void OpenBuildingAttributes(Building building, int id)
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