using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Components;
using Simulation.Common;
using System.Collections.Concurrent;
namespace Simulation.World
{


    public class Building : MonoBehaviour
    {

        public int stage;  // finished paused planned in progress
        private List<Mesh> frames;
        private int frame_iterator;
        private SlotContainer container;
        private MeshFilter meshFilter;
        public string Name;





        public Building(string Name, SlotContainer materials,List<Mesh> frames)
        {
            container = materials;   
            this.frames = frames;
            stage = 0;
            frame_iterator = 0;
            this.Name = Name;

        }
        public Mesh GetPreview()
        {
            return frames[frames.Count - 1];
        }
        public void SetContainer(SlotContainer slot)
        {
            this.container = slot;
        }
        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            //Animate();
        }

        public ConcurrentDictionary<BuildingMaterial, int> GetRemaining()
        {
            return container.FreeSpace();
        }
        public ConcurrentDictionary<BuildingMaterial,int> GetFull()
        {
            return container.GetMax();
        }

        public void Build(int quantity, BuildingMaterial mat)
        {

            container.Put(mat, quantity);
            //check for stage increase
        }

        public void Animate()
        {
            meshFilter.mesh = frames[frame_iterator];
        }


    }
}
