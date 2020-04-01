
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Simulation.Robots
{
    class Bzhmil : Robot
    {


        [SerializeField]
        private List<Transform> Rotors;

        private float rotor_speed = 40f;
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
            StartCoroutine(startmove());
            
        }
        private IEnumerator startmove()
        {
            yield return new WaitForSeconds(5);
            agent.SetDestination(new Vector3(0.5f, 0.3f, 5));
            
        }



    }
}
