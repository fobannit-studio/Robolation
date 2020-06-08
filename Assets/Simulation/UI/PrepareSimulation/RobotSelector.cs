
using Simulation.Robots;
using Simulation.Software;
using Simulation.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Simulation.UI
{
    public class RobotSelector : MonoBehaviour
    {
        private Dictionary<string, Robot> robots_dict;
        private List<string> robots_names;

        [SerializeField]
        private List<RobotListing> listings;

        private void Start()
        {

            Init();


        }
        private void Init()
        {
            robots_dict = new Dictionary<string, Robot>();
            robots_names = new List<string>();
            foreach (var item in FileManager.Robots)
            {
                robots_dict.Add(item.GetType().Name, item);
                robots_names.Add(item.GetType().Name);
            }

            listings[0].Init(typeof(BuilderSoftware), robots_names, robots_dict);
            listings[1].Init(typeof(OperatorSoftware), robots_names, robots_dict);
            listings[2].Init(typeof(TransporterSoftware), robots_names, robots_dict);
        }
        private void OnEnable()
        {
            Init();
        }
        public Dictionary<Type,List<(Robot robot,int amount)>> GetSelectedRobots()
        {
            var result = new Dictionary<Type, List<(Robot robot, int amount)>>();
            foreach (var item in listings)
            {
                result.Add(item.Software,item.GetSelectedRobots());
            }
            return result;
        }

        public bool Proceed()
        {
            var isOk = true;
            foreach (var item in listings)
            {
                isOk = isOk && item.Validate();
            }

            return isOk;


        }

    }
}