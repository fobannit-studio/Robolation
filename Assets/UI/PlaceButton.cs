using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Simulation.World;
namespace Simulation.UI
{
    public class PlaceButton : MonoBehaviour
    { 
        [SerializeField]
        private Text myText;
        [SerializeField]
        private Button myButton;
        private Building assignedBuilding;
        public BuildingPlacer placer;
        private int id;

        public void OnClick()
        {
            if (placer!=null)
            {
                placer.SelectBuilding(assignedBuilding, id);
            }
        }
        public void Init(Building building,BuildingPlacer placer,int id)
        {
            assignedBuilding = building;
            this.placer = placer;
            myText.text = building.Name;
            myButton.onClick.AddListener(OnClick);
            
        }
        private void OnDestroy()
        {
            myButton.onClick.RemoveListener(OnClick);
        }




    }

}