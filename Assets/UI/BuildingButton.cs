using Simulation.World;
using UnityEngine;
using UnityEngine.UI;
namespace Simulation.UI
{
    public class BuildingButton : MonoBehaviour
    {

        [SerializeField]
        private Text myText;
        private Building building;
        private BuildingsEditor editor;
        public int id;

        public void onClick()
        {
            if (editor != null)
                editor.OpenBuildingAttributes(building, id);
        }
        public void SetText(string text)
        {
            myText.text = text;
        }
        private void OnDestroy()
        {
            this.GetComponent<Button>().onClick.RemoveListener(onClick);
        }
        public void init(string text, BuildingsEditor editor, Building building, int id)
        {
            this.building = building;
            myText.text = text;
            this.GetComponent<Button>().onClick.AddListener(onClick);
            this.editor = editor;
            this.id = id;

        }
    }
}