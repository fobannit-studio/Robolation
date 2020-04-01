using Simulation.Robots;
using Simulation.Software;
using Simulation.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Simulation.UI
{
    public class RobotListing : MonoBehaviour
    {
        [SerializeField]
        private GameObject dropdown_example;
        ScrollViewManager<RobotDropdown> dropdowns;
        
        public Type Software { get=>software; }
        private Type software;
        private List<string> RobotsModels;
        private Dictionary<string, Robot> robots_dict;


        private void Start()
        {
            dropdowns = new ScrollViewManager<RobotDropdown>();
        }
        public  void Init(Type soft,List<string> RobotsModels, Dictionary<string, Robot>  robots_dict)
        {
          
            this.robots_dict = robots_dict;
            software = soft;
            this.RobotsModels = RobotsModels;
        }
        public bool Validate()
        {
            int result = 0;
            foreach (var item in dropdowns.elements)
            {
                result += item.GetAmount();
            }

            return result != 0;
        }
        public List<(Robot robot, int amount)> GetSelectedRobots()
        {
            var result = new List<(Robot robot, int amount)>();

            foreach (var item in dropdowns.elements)
            {
                if (item.GetAmount() != 0)
                    result.Add((robots_dict[item.GetOption()], item.GetAmount()));
            }
            return result;
        }



        public void AddRobot()
        {
            var dropdown = dropdowns.GenerateList(dropdown_example);
            dropdown.Init(RobotsModels);
        }
    }
}
