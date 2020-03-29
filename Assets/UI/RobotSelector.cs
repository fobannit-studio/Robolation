
using Simulation.Robots;
using Simulation.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Simulation.UI
{
    public class RobotSelector : MonoBehaviour
    {
        [SerializeField]
        private GameObject builder_button_example;
        ScrollViewManager<RobotDropdown> builder_dropdowns;

        public List<GameObject> robots;

   

        private void Start()
        {
            builder_dropdowns = new ScrollViewManager<RobotDropdown>();
            foreach (var item in FileManager.Robots)
            {
                
            }
           

        }
        public void AddBuilderModel()
        {
            var dropdown=builder_dropdowns.GenerateList(builder_button_example);
           // dropdown.Init()
        }

    }
}