
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Simulation.Robots
{
    class Parrot : Robot
    {


        [SerializeField]
        private List<Transform> Rotors;

        private float rotor_speed = 40f;

        public override int BuildIterations => 15;

        protected override int cointainer_size => 20;

        IEnumerator AnimateRotors()
        {
            for (; ; )
            {
                foreach (var item in Rotors)
                {
                    item.Rotate(new Vector3(0, rotor_speed, 0));
                }
                yield return null;

            }
              
        }

        private void Start()
        {
            StartCoroutine(AnimateRotors());
           
            
        }
       



    }
}
