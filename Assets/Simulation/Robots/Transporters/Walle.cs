using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Robots;
using Simulation.World;

namespace Assets.Simulation.Robots.Transporters
{
    public class Walle : Robot
    {
        [SerializeField]
        private Transform LeftArm;
        [SerializeField]
        private Transform RightArm;
        [SerializeField]
        private Transform Plate;

        public Walle(Vector3 positionInWorld, float radioRange, ref Medium ether) : base(positionInWorld, radioRange, ref ether)
        {
        }

        // Start is called before the first frame update

        /// <summary>
        /// Coroutine for opening plate of Wall-e.
        /// </summary>
        IEnumerator OpenPlate()
        {
            for (float i = 0; i < 1; i+=Time.deltaTime)
            {
                Plate.rotation= Quaternion.Euler( Mathf.Lerp(0,120,i),0, 0);
                yield return null;
            }
            Plate.rotation = Quaternion.Euler(120, 0, 0);
            
        }
        IEnumerator ClosePlate()
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                Plate.rotation = Quaternion.Euler(Mathf.LerpAngle(120, 0, i), 0, 0);
                yield return null;
            }
            Plate.rotation = Quaternion.Euler(0, 0, 0);

        }
      
       
        void Update()
        {
            if (agent.desiredVelocity!=new Vector3(0,0,0))
               transform.rotation = Quaternion.LookRotation(agent.desiredVelocity);
               
        }

      
    }
}