using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
namespace Simulation.UI
{



    public class MaterialDropDown : MonoBehaviour
    {
        [SerializeField]
        private Dropdown dropdown;
        [SerializeField]
        private InputField amount;




        public void Init(List<string> options, int amount,int valueSelected=0)
        {
            this.amount.text = amount.ToString();
            dropdown.AddOptions(options);
            dropdown.value = valueSelected;
    
            
        }
        private void Update()
        {
            if (!GetAmount(out _))
            {
                this.amount.text = "0";
            }
        }


        public bool GetAmount(out int amount)
        {
            return int.TryParse(this.amount.text, out amount);
        }
        public string GetMaterialName()
        {
            return dropdown.options[dropdown.value].text;
        }

    }
}
