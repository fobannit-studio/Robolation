using System;
using UnityEngine;
using System.Collections.Concurrent;
namespace Simulation.Common
{
    // Structures describes a material type. 
    public struct BuildingMaterial
    {
        public readonly string Type;
        public readonly (float x, float y, float z)  Dimensions;
        public readonly float Weight;
        public readonly float Volume;

        public static ConcurrentDictionary<string,BuildingMaterial> existingMaterials=new ConcurrentDictionary<string, BuildingMaterial>();

        public BuildingMaterial(string type, (float x, float y, float z) dimensions, float weight)
        {
            
            Type = type;
            Dimensions = dimensions;
            Volume = dimensions.x * dimensions.y * dimensions.z;
            Weight = weight;
        }

        public override string ToString()
        {
            return Type;
        }
        
    }

}