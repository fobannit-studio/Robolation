using Simulation.Robots;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Simulation.UI
{
    public class ProgressBard3D : MonoBehaviour
    {
        [SerializeField]
        private Robot robot;
 

        private Slider bar;

        private Camera cam;
        private void Start()
        {  
           cam = Camera.main;

        }
        public void SetBar(Slider slider)
        {
            bar = slider;
            bar.maxValue = robot.BuildIterations;
            bar.value = robot.IterationsPassed;
            

        }
     

        void Update()
        {
           
            Vector3 namepos = cam.WorldToScreenPoint(this.transform.position);
            bar.gameObject.SetActive(robot.IterationsPassed!=0);

            bar.value = robot.IterationsPassed;
            bar.transform.position = namepos;
        }
    }
}