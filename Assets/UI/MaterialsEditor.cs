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


        private ScrollViewManager<MaterialButton> aviable_mats;

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
            aviable_mats = new ScrollViewManager<MaterialButton>();
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
                AddAviableMats();
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
            aviable_mats.ClearList();
            for (int i = 0; i < materials.Count; i++)
            {             
                var materialButton = aviable_mats.GenerateList(this.MaterialButtonTemplate);
                materialButton.init(materials[i].Type, this, materials[i], i);
            }
            
        }
        void Update()
        {
            var Dimensions = (0f, 0f, 0f);     
            if (Parsed(out Dimensions, out _))
                ResizeCube(Dimensions);
            
        }
        
    }
}