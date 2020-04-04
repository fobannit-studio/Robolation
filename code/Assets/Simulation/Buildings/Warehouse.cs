using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Simulation.World
{
    public class Warehouse : MonoBehaviour
    {
        public SlotContainer container;

        [SerializeField]
        private MeshCollider meshCollider;

        public void SetPreview(bool val)
        {
            this.meshCollider.enabled = !val;
        }
        

    }
}