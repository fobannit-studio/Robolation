using System;
using UnityEngine;
namespace Simulation.Common
{
    // Structures describes a material type. 
    public struct BuildingMaterial
    {
        public readonly string Type;
        public readonly (float x, float y, float z)  Dimensions;
        public readonly float Weight;
        public readonly float Volume;
        public BuildingMaterial(string type, (float x, float y, float z) dimensions, float weight)
        {
            Type = type;
            Dimensions = dimensions;
            Volume = dimensions.x * dimensions.y * dimensions.z;
            Weight = weight;
        }
    }

}