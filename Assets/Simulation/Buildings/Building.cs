using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using System.Collections.Concurrent;
using System;
namespace Simulation.World
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private MeshCollider collider;
        [SerializeField]
        private MeshFilter meshFilter;


        /// <summary>
        /// Return the closest position to point given as first argument
        /// </summary>
        public Func<Vector3, Vector3> ClosestPoint;
        public int stage;  // finished, paused, planned, in progress
        private List<Mesh> frames;
        private int frame_iterator;
        private SlotContainer container;
       
        public string Name;

        public void Init(string Name, SlotContainer materials,List<Mesh> frames)
        {

            container = materials;   
            this.frames = frames;
            stage = 0;
            frame_iterator = 0;
            this.Name = Name;
            ClosestPoint = this.GetComponent<MeshRenderer>().bounds.ClosestPoint;
        }
        public  Building(string Name, SlotContainer materials, List<Mesh> frames)
        {
            container = materials;
            this.frames = frames;
            stage = 0;
            frame_iterator = 0;
            this.Name = Name;
        }
        public SlotContainer GetSlotContainer()
        {
            return container;
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
            //meshFilter = GetComponent<MeshFilter>();
            //Animate();
        }
        public List<Mesh> GetFrames()
        {
            return this.frames;
        }
        public void RecalculateCollider(Mesh mesh)
        {
            collider.sharedMesh = mesh;
        }
        public void SetPreview()
        {
            meshFilter.mesh = frames[frames.Count - 1];
            collider.enabled = false;
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
