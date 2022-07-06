using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace.Generation
{
    public class TilemapChunk : MonoBehaviour
    {
        public Tilemap Tilemap;
        public List<Vector3Int> tileWorldLocations;
        public IslandType Type;

        public void GenerateTiles()
        {
            tileWorldLocations = new List<Vector3Int>();

            foreach (var pos in Tilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if (Tilemap.HasTile(localPlace))
                {
                    tileWorldLocations.Add(localPlace);
                }
            }
            GetHeight();
            GetWidth();
        }

        public void GetHeight()
        {
            int maxY = -1000;
            int minY = 10000;
            foreach (var tile in tileWorldLocations)
            {
                if (tile.y > maxY) maxY = tile.y;
                if (tile.y < minY) minY = tile.y;
            }

            Height = maxY - minY;
        }

        public void GetWidth()
        {
            int maxX = -1000;
            int minX = 10000;
            foreach (var tile in tileWorldLocations)
            {
                if (tile.x > maxX) maxX = tile.x;
                if (tile.x < minX) minX = tile.x;
            }


            Width = maxX - minX;
        }

        public int Height;
        public int Width;

        public enum IslandType
        {
            Small,
            Medium,
            Large
        }
    }
}