using Simulation.Robots;
using Simulation.Utils;
using Simulation.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Simulation.UI
{
    public class SimulationHUD : MonoBehaviour
    {
        private Camera cam;
        public float mouseSensitivity;
        private bool isActive;

        float speed = 50;
        float xRotation;

        [SerializeField]
        private GameObject HUDCanvas;


        ScrollViewManager<MonoBehaviour> containerView;
        [SerializeField]
        private GameObject ContainerExample;

        private IContainer container;
        private bool isChanged;
        private int container_len;

        [SerializeField]
        private GameObject BarExample;

        void Start()
        {
            //Activate();
            cam = GetComponent<Camera>();
            containerView = new ScrollViewManager<MonoBehaviour>();
            isChanged = false;
            container_len = 0;
            
        }

        // Update is called once per frame
        void ShowContainer(IContainer container)
        {
            containerView.ClearList();
            foreach (var item in container.GetContent())
            {
                var elem=containerView.AddElement(ContainerExample);
                string text= $"{item.Key.Type.ToUpper()}: {item.Value} unit";
                if (item.Value != 1)
                    text += "s";
                elem.GetComponent<Text>().text = text;
            }
            isChanged = false;
            container_len= container.GetContent().Values.Sum();

        }
        void SetContainer(IContainer container)
        {
            this.container = container;
            ShowContainer(container);
        }
        void CheckContainer()
        {
            if (container!=null)
              isChanged = container_len != container.GetContent().Values.Sum();

            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                

                var warehouse = hit.collider.GetComponent<Warehouse>();
                if (warehouse)
                {
                    SetContainer(warehouse.container);
                    return;
                }
                var robot = hit.collider.GetComponent<Robot>();
                if (robot)
                {
                    SetContainer(robot.MaterialContainer);
                    return;
                }
                var house = hit.collider.GetComponent<Building>();
                if (house)
                {
                    SetContainer(house.GetSlotContainer());
                    return;
                }
            }


        }

        void Update()
        {
            if (!isActive) return;
            MouseLook();

            Moving();
            CheckContainer();

            if (isChanged)
                ShowContainer(container);




        }
        void MouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);


            transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
        void Moving()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            float y = 0;
            if (Input.GetKey(KeyCode.Space))
                y = 1;
            else if (Input.GetKey(KeyCode.LeftControl))
                y = -1;




            Vector3 move = transform.right * x + transform.forward * z + transform.up * y;
            this.transform.position = transform.position + move * Time.deltaTime * speed;
        }
        public void Activate()
        {
            isActive = true;
            Cursor.lockState = CursorLockMode.Locked;
            xRotation = transform.eulerAngles.x;
            HUDCanvas.SetActive(true);


            var bars = FindObjectsOfType<ProgressBard3D>();
            foreach (var item in bars)
            {
                Debug.Log("updating");
                var bar = Instantiate(BarExample);
                bar.transform.parent = HUDCanvas.transform;
                bar.SetActive(true);
                item.SetBar(bar.GetComponent<Slider>());
            }

        }
    }
}