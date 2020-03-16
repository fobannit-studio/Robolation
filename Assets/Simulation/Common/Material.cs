using System;
using UnityEngine;
namespace Simulation.Common
{
    // Structures describes a material type. 
    struct BuildingMaterial
    {
        public readonly string Type;
        public readonly (int x, int y, int z)  Dimensions;
        public readonly int Weight;
        public readonly int Size;
        public BuildingMaterial(string type, (int x, int y, int z) dimensions, int weight)
        {
            Type = type;
            Dimensions = dimensions;
            Size = dimensions.x * dimensions.y * dimensions.z;
            Weight = weight;
        }
    }

}