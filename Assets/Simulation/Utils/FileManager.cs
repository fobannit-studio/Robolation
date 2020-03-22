using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using System.IO;
using Simulation.World;

namespace Simulation.Utils
{
    public class FileManager
    {
        private const string MaterialsDir = "BuildingMaterials";

        private const string BuildingsDir = "Buildings";

        private const string MaterialsFile = "materials";

        private const string ExampleMesh = "Assets\\Models\\Small House\\house_3.obj";


        public static List<BuildingMaterial> ReadMaterials()
        {
            BuildingMaterial.existingMaterials = new System.Collections.Concurrent.ConcurrentDictionary<string, BuildingMaterial>();
            List<BuildingMaterial> materials = new List<BuildingMaterial>();
            if (Directory.Exists(MaterialsDir))
            {

                foreach (string file in Directory.GetFiles(MaterialsDir))
                {

                    var mat = File.ReadAllText(file);
                    var separated = mat.Split(',');
                    var type = Path.GetFileName(file);
                    var x = float.Parse(separated[0]);
                    var y = float.Parse(separated[1]);
                    var z = float.Parse(separated[2]);
                    var weight = float.Parse(separated[3]);
                    var material = new BuildingMaterial(type, (x, y, z), weight);
                    materials.Add(material);
                    BuildingMaterial.existingMaterials.TryAdd(material.Type, material);
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
            BuildingMaterial.existingMaterials = new System.Collections.Concurrent.ConcurrentDictionary<string, BuildingMaterial>();
            if (!Directory.Exists(MaterialsDir))
            {
                Directory.CreateDirectory(MaterialsDir);
            }
            foreach (var mat in materials)
            {
                File.WriteAllText(MaterialsDir + @"/" + mat.Type, mat.Dimensions.x + "," + mat.Dimensions.y + "," + mat.Dimensions.z + "," + mat.Weight);
                BuildingMaterial.existingMaterials.TryAdd(mat.Type, mat);
            }


        }
        private static SlotContainer ParseBuildingMaterials(string filename)
        {
            var result = new SlotContainer();
            var content = File.ReadAllLines(filename);

            BuildingMaterial material = new BuildingMaterial();
            int amount;
            for (int i = 0; i < content.Length; i++)
            {
                if (i % 2 == 0)
                    material = BuildingMaterial.existingMaterials[content[i]];
                else
                {
                    amount = int.Parse(content[i]);
                    result.AddSlot(material, amount);
                }
            }

            return result;
        }

        public static List<Building> ReadBuildings()
        {
            if (!Directory.Exists(BuildingsDir))
            {
                Directory.CreateDirectory(BuildingsDir);
                return new List<Building>();
            }
            var result = new List<Building>();
            foreach (var build_dir in Directory.GetDirectories(BuildingsDir))
            {

                if (!File.Exists(build_dir + @"\" + MaterialsFile))
                    return new List<Building>();

                var files = Directory.GetFiles(build_dir);
                SlotContainer slotContainer = ParseBuildingMaterials(build_dir + @"\" + MaterialsFile);
                ObjImporter importer = new ObjImporter();
                List<Mesh> meshes = new List<Mesh>();
                for (int i = 0; i < files.Length - 1; i++)
                {
                    meshes.Add(importer.ImportFile(files[i]));
                }
                var building = new Building(Path.GetFileName(build_dir), slotContainer, meshes);

                result.Add(building);

            }
            return result;


        }
   

        public static void SaveBuildings(List<Building> buildings)
        {

            if (!Directory.Exists(BuildingsDir))

                Directory.CreateDirectory(BuildingsDir);

            

            foreach (var building in buildings)
            {

                if (!Directory.Exists(BuildingsDir + @"\" + building.Name))
                {
                    Directory.CreateDirectory(BuildingsDir + @"\" + building.Name);
                    File.Copy(ExampleMesh, BuildingsDir + @"\" + building.Name + @"\" + "0.obj");
                }
                    

                var materials = building.GetFull();

                var content=new List<string>();
                foreach (var mat in materials)
                {
                    content.Add(mat.Key.Type);
                    content.Add(mat.Value.ToString());
                }

                File.WriteAllLines(BuildingsDir + @"\" + building.Name + @"\" + MaterialsFile,content);

               





            }


        }
    }
}
