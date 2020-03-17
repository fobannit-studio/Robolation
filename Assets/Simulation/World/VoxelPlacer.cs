using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


using Simulation.Common;

namespace Simulation.World
{


    public class VoxelPlacer : MonoBehaviour
    {
        public List<VoxelTile> TilePrefabs;

        private VoxelTile[,] spawnedTiles;
        public Vector2Int Mapsize = new Vector2Int(5, 5);

        private void Start()
        {
            spawnedTiles = new VoxelTile[Mapsize.x, Mapsize.y];
            foreach (VoxelTile tile in TilePrefabs)
            {
                tile.CalculateColors();
            }
            int offset = 8;
            int countBeforeAdding = TilePrefabs.Count;
            for (int i = 0; i < countBeforeAdding; i++)
            {
                VoxelTile clone;
                switch (TilePrefabs[i].Rotation)
                {
                    case VoxelTile.RotationType.OnlyRotation:
                        break;
                    case VoxelTile.RotationType.TwoRotations:
                        TilePrefabs[i].Weight /= 2;
                        if (TilePrefabs[i].Weight <= 0) TilePrefabs[i].Weight = 0;

                        clone = Instantiate(TilePrefabs[i], TilePrefabs[i].transform.position + Vector3.right * offset * TilePrefabs[i].Voxels * TilePrefabs[i].VoxelSize, Quaternion.identity);
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        offset++;
                        break;
                    case VoxelTile.RotationType.FourRotations:

                        TilePrefabs[i].Weight /= 4;
                        if (TilePrefabs[i].Weight <= 0) TilePrefabs[i].Weight = 0;

                        clone = Instantiate(TilePrefabs[i], TilePrefabs[i].transform.position + Vector3.right * TilePrefabs[i].Voxels * offset * TilePrefabs[i].VoxelSize, Quaternion.identity);
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        offset++;
                        clone = Instantiate(TilePrefabs[i], TilePrefabs[i].transform.position + Vector3.right * TilePrefabs[i].Voxels * offset * TilePrefabs[i].VoxelSize, Quaternion.identity);
                        clone.Rotate90();
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        offset++;
                        clone = Instantiate(TilePrefabs[i], TilePrefabs[i].transform.position + Vector3.right * TilePrefabs[i].Voxels * offset * TilePrefabs[i].VoxelSize, Quaternion.identity);
                        clone.Rotate90();
                        clone.Rotate90();
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        offset++;
                        break;
                    default:
                        break;
                }
            }
            // Debug.Log(TilePrefabs.Count);

            StartCoroutine(Generate());

        }
        private VoxelTile GetRandomTile(List<VoxelTile> aviableTiles)
        {
            List<float> chances = new List<float>();
            for (int i = 0; i < aviableTiles.Count; i++)
            {
                chances.Add(aviableTiles[i].Weight);
            }
            float value = Random.Range(0, chances.Sum());
            float sum = 0;
            for (int i = 0; i < chances.Count; i++)
            {
                sum += chances[i];
                if (value < sum)
                {
                    return aviableTiles[i];
                }

            }
            return aviableTiles[aviableTiles.Count - 1];


        }
        public void PlaceTile(int x, int y)
        {
            List<VoxelTile> aviableTiles = new List<VoxelTile>();
            foreach (VoxelTile tile in TilePrefabs)
            {
                if (CandAppendTile(spawnedTiles[x - 1, y], tile, Direction.Forward) &&
                CandAppendTile(spawnedTiles[x + 1, y], tile, Direction.Back) &&
                CandAppendTile(spawnedTiles[x, y + 1], tile, Direction.Right) &&
                CandAppendTile(spawnedTiles[x, y - 1], tile, Direction.Left))
                {
                    aviableTiles.Add(tile);

                }
            }

            if (aviableTiles.Count == 0)
            {
                // Debug.Log("no aviable");
                return;
            }
            VoxelTile selectedTile = GetRandomTile(aviableTiles);
            // Debug.Log(aviableTiles);
            Vector3 position = selectedTile.Voxels * selectedTile.VoxelSize * new Vector3(x, 0, y);
            spawnedTiles[x, y] = Instantiate(selectedTile, position, selectedTile.transform.rotation);


        }
        private IEnumerator Generate()
        {
            for (int i = 1; i < Mapsize.x - 1; i++)
            {
                for (int j = 1; j < Mapsize.y - 1; j++)
                {
                    yield return new WaitForSeconds(0.0001f);
                    PlaceTile(i, j);
                }
            }


        }
        private bool CandAppendTile(VoxelTile existingTile, VoxelTile tileToAppend, Direction direction)
        {
            if (existingTile == null) return true;
            // Debug.Log("not null");


            if (direction == Direction.Right)
            {
                // Debug.Log("right direction");
                return Enumerable.SequenceEqual(existingTile.ColorsRight, tileToAppend.ColorsLeft);

            }

            else if (direction == Direction.Left)
            {
                // Debug.Log("left direction");
                return Enumerable.SequenceEqual(existingTile.ColorsLeft, tileToAppend.ColorsRight);
            }

            else if (direction == Direction.Forward)
            {
                // Debug.Log("forward direction");
                return Enumerable.SequenceEqual(existingTile.ColorsForward, tileToAppend.ColorsBack);
            }

            else if (direction == Direction.Back)
            {
                // Debug.Log("back direction");
                return Enumerable.SequenceEqual(existingTile.ColorsBack, tileToAppend.ColorsForward);
            }

            else
                throw new System.ArgumentException("wrong dirrection value, only Vector3.forwad/back/right/left ");



        }
    }
}