using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Simulation.Common;
namespace Simulation.UI
{
    public class MaterialButton : MonoBehaviour
    {
        [SerializeField]
        private Text myText;

        private BuildingMaterial material;
        private MaterialsEditor editor;
        public int id;
        
        public  void onClick()
        {   if (editor!=null)
            editor.OpenMaterialAttr(this.material,id);
        }
        public void SetText(string text)
        {
            myText.text = text;
        }
        private void OnDestroy()
        {
            this.GetComponent<Button>().onClick.RemoveListener(onClick);
        }
        public void init (string text,MaterialsEditor editor, BuildingMaterial mat,int id)
        {
            this.material = mat;
            myText.text = text;
            this.GetComponent<Button>().onClick.AddListener(onClick);
            this.editor = editor;
            this.id = id;

        }
    }
}
