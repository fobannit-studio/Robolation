using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using System.Collections.Concurrent;
using System;
using Simulation.Utils;
using System.Security.AccessControl;
using System.Collections.Concurrent;
using Unity.Collections.LowLevel.Unsafe;

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
        public bool IsFinished { get; set; } = false;
        public void Init(string Name, SlotContainer materials,List<Mesh> frames)
        {

            container = materials;   
            this.frames = frames;
            stage = 0;
            frame_iterator = 0;
            this.Name = Name;
            ClosestPoint = this.GetComponent<MeshRenderer>().bounds.ClosestPoint;
        }

        private int RecalculateMaterial(ConcurrentDictionary<BuildingMaterial, int> content)
        { 
            int cnt = 0;
            foreach( var item in content) 
            {
                cnt += item.Value;
            }
            return cnt;
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
            SetFrame(frames.Count-1);
            collider.enabled = false;
        }
        public void SetActual()
        {
            collider.enabled = true;
            SetFrame(frame_iterator);
        }
        public ConcurrentDictionary<BuildingMaterial, int> GetRemaining()
        {
            return container.FreeSpace();
        }
        public ConcurrentDictionary<BuildingMaterial,int> GetFull()
        {
            return container.GetMax();
        }

        public void Build(IContainer container)
        {
            Debug.Log("Before building");

            foreach (var item in this.container.GetContent())
            {
                Debug.LogFormat("{0} units of {1}", item.Value, item.Key.Type);
            }

            foreach (var item in container.GetContent())
            {
                
                if (this.container.GetMax().TryGetValue(item.Key,out int _need))
                {
                
                    container.TryTransferTo(this.container, item.Key, item.Value);
                }

              
            }
            Debug.Log("After building");
            foreach (var item in this.container.GetContent())
            {
                Debug.LogFormat("{0} units of {1}", item.Value, item.Key.Type);
            }
            // сделать анимацию
            int have = RecalculateMaterial(this.container.GetContent());
            int need =RecalculateMaterial(this.container.GetMax());
            SetFrame((have  * (frames.Count - 1)/ need));
        }
        public void SetFrame(int frame)
        {
            frame_iterator = frame;
            Animate();
        }
        public void Animate()
        {
            meshFilter.mesh = frames[frame_iterator];
        }
        


    }
}
