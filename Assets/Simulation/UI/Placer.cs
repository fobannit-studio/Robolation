using UnityEngine;
namespace Simulation.UI
{


    public delegate void OnPlacedAction(GameObject instantiated);

    public class Placer : MonoBehaviour
    {



        private GameObject currentObject;
        private Camera cam;


        private OnPlacedAction placedAction;
        private float mouseWheelRotation;


        private void Start()
        {
            cam = Camera.main;
        }
        public void ChangeObject(GameObject obj)
        {
            if (currentObject != null)
            {
                Destroy(currentObject.gameObject);
                currentObject = null;
            }
            currentObject = obj;
        }
        public void Init(OnPlacedAction onPlaced)
        {
            placedAction = onPlaced;
       
        }

        private void Update()
        {
            if (currentObject != null)
            {
                MoveToMouse();
                RotateFromMouseWheel();
                ReleaseIfClicked();
            }
        }

        private void ReleaseIfClicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    placedAction(currentObject);
                    currentObject = null;
                }
            }
        }

        private void MoveToMouse()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                currentObject.transform.position = hit.point;
                currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }

        }

        private void RotateFromMouseWheel()
        {
            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }
    }

}