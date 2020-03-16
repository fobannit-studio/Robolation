using UnityEngine;
namespace Simulation.Common
{
    struct Material{
        public Vector3 Dimensions;
        public int Weight;
        public string Type;

        Material(Vector3 dim, int weight, string type){
            Weight = weight;
            Dimensions = dim;
            Type = type;

        }

        public float Size{
            get{
                return Dimensions.x * Dimensions.y * Dimensions.z;
            }
        }


    }
}