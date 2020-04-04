using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Simulation.Common;

namespace Simulation.World
{
    public class VoxelTile : MonoBehaviour
    {
        public float VoxelSize = 0.1f;
        public int Voxels = 8;
        [Range(1, 100)]
        public int Weight = 50;

        public byte[] ColorsRight;
        public byte[] ColorsLeft;
        public byte[] ColorsForward;
        public byte[] ColorsBack;
        public RotationType Rotation;
        public enum RotationType
        {
            OnlyRotation,
            TwoRotations,
            FourRotations
        }

        // Start is called before the first frame update
        void Start()
        {


        }
        public void Rotate90()
        {
            transform.Rotate(0, 90, 0);

            byte[] ColorsRightNew = new byte[Voxels * Voxels];
            byte[] ColorsLeftNew = new byte[Voxels * Voxels];
            byte[] ColorBackNew = new byte[Voxels * Voxels];
            byte[] ColorsForwardNew = new byte[Voxels * Voxels];
            for (int layer = 0; layer < Voxels; layer++)
            {
                for (int offset = 0; offset < Voxels; offset++)
                {
                    ColorsRightNew[layer * Voxels + offset] = ColorsForward[layer * Voxels + offset];
                    ColorsForwardNew[layer * Voxels + offset] = ColorsLeft[layer * Voxels + Voxels - offset - 1];
                    ColorsLeftNew[layer * Voxels + offset] = ColorsBack[layer * Voxels + offset];
                    ColorBackNew[layer * Voxels + offset] = ColorsRight[layer * Voxels + Voxels - offset - 1];

                }
            }
            ColorsRight = ColorsRightNew;
            ColorsForward = ColorsForwardNew;
            ColorsLeft = ColorsLeftNew;
            ColorsBack = ColorBackNew;

        }
        public void CalculateColors()
        {
            ColorsBack = new byte[Voxels * Voxels];
            ColorsForward = new byte[Voxels * Voxels];
            ColorsLeft = new byte[Voxels * Voxels];
            ColorsRight = new byte[Voxels * Voxels];
            for (int i = 0; i < Voxels; i++)
            {
                for (int j = 0; j < Voxels; j++)
                {
                    ColorsBack[i * Voxels + j] = (byte)GetVoxelColor(i, j, Direction.Right);
                    ColorsForward[i * Voxels + j] = (byte)GetVoxelColor(i, j, Direction.Left);
                    ColorsLeft[i * Voxels + j] = (byte)GetVoxelColor(i, j, Direction.Back);
                    ColorsRight[i * Voxels + j] = (byte)GetVoxelColor(i, j, Direction.Forward);

                }
            }
        }


        private int GetVoxelColor(int verticalLayer, int horizontalOffset, Direction direction)
        {
            MeshCollider mesh = GetComponent<MeshCollider>();
            Vector3 raystart;
            Vector3 raydir;
            float half = VoxelSize / 2;
            float vox = VoxelSize;

            if (direction == Direction.Forward)
            {
                raystart = mesh.bounds.min +
                   new Vector3(+half + horizontalOffset * vox, 0, -half);
                raydir = Vector3.forward;
            }
            else if (direction == Direction.Right)
            {
                raystart = mesh.bounds.min +
                   new Vector3(-half, 0, half + horizontalOffset * vox);
                raydir = Vector3.right;
            }
            else if (direction == Direction.Left)
            {

                raystart = mesh.bounds.max +
               new Vector3(half, 0, -half - (Voxels - 1 - horizontalOffset) * vox);
                raydir = Vector3.left;
            }
            else
            {
                raystart = mesh.bounds.max +
                    new Vector3(-half - (Voxels - 1 - horizontalOffset) * vox, 0, half);
                raydir = Vector3.back;
            }


            raystart.y = mesh.bounds.min.y + half + verticalLayer * vox;

            //  Debug.DrawRay(raystart, direction*0.1f, Color.blue,2);
            if (Physics.Raycast(new Ray(raystart, raydir), out RaycastHit hit, VoxelSize))
            {
                int hitTriangleVertex = mesh.sharedMesh.triangles[hit.triangleIndex * 3 + 0];
                int colorindex = (int)(mesh.sharedMesh.uv[hitTriangleVertex].x * 256);

                return colorindex;

            }
            return 0;


        }

    }
}
