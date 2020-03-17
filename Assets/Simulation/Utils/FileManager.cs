using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using System.IO;
namespace Simulation.Utils
{
    public class FileManager
    {
        private const string MaterialsDir = "BuildingMaterials";
        public static List<BuildingMaterial> ReadMaterials()
        {
            List<BuildingMaterial> materials = new List<BuildingMaterial>();
            if (Directory.Exists(MaterialsDir))
            {

                foreach (string file in Directory.GetFiles(MaterialsDir))
                {

                    var mat = File.ReadAllText(file);
                    var separated = mat.Split(',');
                    var type = Path.GetFileName(file) ;
                    var x = float.Parse(separated[0]);
                    var y = float.Parse(separated[1]);
                    var z = float.Parse(separated[2]);
                    var weight = float.Parse(separated[3]);

                    materials.Add(new BuildingMaterial(type, (x, y, z), weight));
                }
                
            }
            else
            {
                Directory.CreateDirectory(MaterialsDir);
            }
            return materials;
        }
        public static void SaveMaterials(List<BuildingMaterial> materials)
        {
            if (!Directory.Exists(MaterialsDir))
            {
                Directory.CreateDirectory(MaterialsDir);
            }
            foreach (var mat in materials)
            {
                File.WriteAllText(MaterialsDir+@"/"+mat.Type, mat.Dimensions.x + "," + mat.Dimensions.y + "," + mat.Dimensions.z + "," + mat.Weight);
            }
        }

    }
}
