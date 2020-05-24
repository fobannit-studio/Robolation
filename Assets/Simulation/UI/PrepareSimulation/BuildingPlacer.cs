using Simulation.Utils;
using Simulation.World;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simulation.UI
{

    public class BuildingPlacer : MonoBehaviour
    {



        [SerializeField]
        private GameObject buildingPrefab;


        [SerializeField]
        private GameObject ExampleButton;

        private List<GameObject> buttons;
        private List<Building> buildings;


        [SerializeField]
        private Transform Landscape;
        public static List<Building> placedBuildings= new List<Building>();

        [SerializeField]
        private Button nextButton;

       
        private Placer placer;



        void Start()
        {
            

            FileManager.ReadMaterials();
            buildings = FileManager.ReadBuildings();
            placer = gameObject.AddComponent<Placer>();
            placer.Init(BuildingPlaced);
            buttons = new List<GameObject>();
           
            CreateList();

            
        }

        private void BuildingPlaced(GameObject instantiated)
        {
            var building = instantiated.GetComponent<Building>(); 
            placedBuildings.Add(building);
            nextButton.interactable = true;
            
            
        }
        
        public void SelectBuilding(Building example_building,int id)
        {
          
            var building = buildingPrefab.GetComponent<Building>();
            building.Init(example_building.Name, example_building.GetSlotContainer(), example_building.GetFrames());
            building.SetPreview();

            placer.ChangeObject(Instantiate(buildingPrefab,Landscape));
        }
        public List<Building> GetBuildings()
        {
            return placedBuildings;
        }
        private void CreateList()
        {
            for (int i = 0; i < buildings.Count; i++)
            {

                GameObject button = Instantiate(ExampleButton) as GameObject;
                button.SetActive(true);
                var placebutton = button.GetComponent<PlaceButton>();
                button.transform.SetParent(ExampleButton.transform.parent, false);
                buttons.Add(button);


                placebutton.Init(buildings[i], this, i);
               
            }
        }

     
    }
}