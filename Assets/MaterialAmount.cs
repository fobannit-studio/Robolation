using UnityEngine.UI;
using UnityEngine;
using Simulation.Common;
namespace Simulation.UI
{
    public class MaterialAmount : MonoBehaviour
    {
        [SerializeField]
        private Text material_text;
        [SerializeField]
        private InputField amount;
        public BuildingMaterial assigned_material;

        
        
        
        public int Amount()
        {
            if (int.TryParse(this.amount.text, out int result))
            {
                return result;
            }
            else
            {
                this.amount.text = "0";
                return 0;
            }
            
        }

        public void Init(BuildingMaterial buildingMaterial,int amount=0)
        {
            this.material_text.text = buildingMaterial.Type;
            this.assigned_material = buildingMaterial;      
            this.amount.text = amount.ToString();
        }

    }
}
