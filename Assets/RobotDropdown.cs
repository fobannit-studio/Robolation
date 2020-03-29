using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Simulation.UI
{
   
    public class RobotDropdown : MonoBehaviour
    {
        [SerializeField]
        InputField amount;
        [SerializeField]
        Dropdown aviable_robots;

        public void Init(List<string> options)
        {
            aviable_robots.AddOptions(options);

        }
      
    }
}