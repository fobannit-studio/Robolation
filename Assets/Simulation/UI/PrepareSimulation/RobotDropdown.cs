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

        public int GetAmount()
        {
            int result = 0;
            if (int.TryParse(amount.text,out result))
            {
                return result;
            }
            else
            {
                amount.text = "0";
                return 0;
            }
        }
        public void Init(List<string> options)
        {
            aviable_robots.AddOptions(options);

        }
        public string GetOption()
        {
            return aviable_robots.options[aviable_robots.value].text;
        }
      
    }
}