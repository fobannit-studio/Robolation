using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using UnityEngine.UI;
using Simulation.Utils;
namespace Simulation.UI
{

    
    public class MaterialsEditor : MonoBehaviour
    {

        [SerializeField]
        private Manager manager;

        [SerializeField]
        private GameObject MaterialButtonTemplate;
        List<BuildingMaterial> materials;

        public GameObject SampleCube;


        private List<GameObject> buttons;

        [SerializeField]
        private InputField Type;
        [SerializeField]
        private InputField X;
        [SerializeField]
        private InputField Y;
        [SerializeField]
        private InputField Z;
        [SerializeField]
        private InputField Weight;

        

        private int editing;

        public void OpenEditor()
        {
            gameObject.SetActive(true);
            editing = 0;
            materials = FileManager.ReadMaterials();
            buttons = new List<GameObject>();
            SampleCube.SetActive(true);
            AddAviableMats();

        }
        
        public void OpenMaterialAttr(BuildingMaterial mat,int id)
        {
            Type.text = mat.Type;
            X.text = mat.Dimensions.x.ToString();
            Y.text = mat.Dimensions.y.ToString();
            Z.text = mat.Dimensions.z.ToString();
            Weight.text = mat.Weight.ToString();

            editing = id;

        }
        public void AddMaterial()
        {
            this.materials.Add(new BuildingMaterial("New Material", (1, 1, 1), 1));
            RefreshMats();

        }
        void RefreshMats()
        {
            foreach (var item in buttons)
            {
                Destroy(item);
            }
            buttons = new List<GameObject>();
            AddAviableMats();
        }
        public void ResizeCube((float x, float y, float z) Dimensions)
        {
            SampleCube.transform.localScale = new Vector3(Dimensions.x, Dimensions.y, Dimensions.z);
        }
        bool Parsed(out  (float x, float y, float z) Dimensions,out float Oweight)
        {
            string type = Type.text;
            float x, y, z, weight;
            Dimensions = (0, 0, 0);
            Oweight = 0;
            bool success= float.TryParse(X.text, out x);

            if (!success)
            {
                X.text = "";
                return false;
            }

            success = float.TryParse(Y.text, out y);

            if (!success)
            {
                Y.text = "";
                return false;
            }
            success = float.TryParse(Z.text, out z);

            if (!success)
            {
                Z.text = "";
                return false;
            }
            success = float.TryParse(Weight.text, out weight);

            if (!success)
            {
                Weight.text = "";
                return false;
            }

            Dimensions = (x, y, z);
            Oweight = weight;
            return true;


        }
        public void SaveClicked()
        {

            float weight;
            var Dimensions = (0f, 0f, 0f);
            if (Parsed(out Dimensions,out weight))
            {
                materials[editing] = new BuildingMaterial(Type.text, Dimensions, weight);
                RefreshMats();
                FileManager.SaveMaterials(materials);
            }
        }
        public void Close()
        {
            SampleCube.SetActive(false);
            this.gameObject.SetActive(false);
            
            manager.ShowMainMenu();
            
            

        }
        public void AddAviableMats()
        {
            for (int i = 0; i < materials.Count; i++)
            {

                GameObject button = Instantiate(MaterialButtonTemplate) as GameObject;
                button.SetActive(true);
                var tmp = button.GetComponent<MaterialButton>();
                tmp.init(materials[i].Type, this, materials[i],i);
                
                button.transform.SetParent(MaterialButtonTemplate.transform.parent, false);
                buttons.Add(button);
            }
            
        }
        void Update()
        {
            var Dimensions = (0f, 0f, 0f);
            float weight = 0;
            if (Parsed(out Dimensions, out weight))
                ResizeCube(Dimensions);
            
        }
        
    }
}