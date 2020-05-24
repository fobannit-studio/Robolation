using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Simulation.UI
{
    public class Cycle : MonoBehaviour
    {
        [SerializeField]
        private Simulation simulation;

       
      
        private Light myLight;


        [SerializeField]
        private Color nightColor;
        [SerializeField]
        private Color daycolor;

        private void Start()
        {
            myLight = GetComponent<Light>();

        }


        private void Update()
        {
        //    if (!simulation.CycleActive) return;

        //    float brightness = 0;
        //    if (simulation.Time < Simulation.dayduration / 2)
        //        brightness = 1 - (simulation.Time / Simulation.dayduration / 2);
        //    else
        //        brightness = (simulation.Time -  Simulation.dayduration/2)/ Simulation.dayduration / 2;

        //    myLight.intensity =  brightness;

        //    myLight.color = Color.Lerp( nightColor, daycolor, brightness);




        }

    }
}
