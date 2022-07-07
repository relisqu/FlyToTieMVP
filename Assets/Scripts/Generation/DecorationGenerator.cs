using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class DecorationGenerator : MonoBehaviour
    {
        [SerializeField] private List<TilemapChunk> TilemapChunks;
        [SerializeField] private GameObject DecorationObject;
        [SerializeField] private Tilemap Tilemap;

        [SerializeField] private float MinDistanceBetweenChunks;
        [SerializeField] private float MaxDistanceBetweenChunks;
        [SerializeField] private float LevelWidth;
        [SerializeField] private float Height;

        void AddTilesToTilemapChunks(TilemapChunk chunk, Vector3Int position)
        {
            chunk.GenerateTiles();
            foreach (var tile in chunk.tileWorldLocations)
            {
                Tilemap.SetTile(tile + position, chunk.Tilemap.GetTile(tile));
            }
        }

        
        [Button("Generate Islands")]
        public void GenerateIslands()
        {
            int children = transform.childCount;
            for (int i = children - 1; i > 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            Tilemap.ClearAllTiles();
            var currentX = 0f;
            while (currentX < LevelWidth)
            {
                var chunk = TilemapChunks[Random.Range(0, TilemapChunks.Count)];
                chunk.GenerateTiles();
                var shift = Random.Range(MinDistanceBetweenChunks, MaxDistanceBetweenChunks);
                var y = Random.Range(0, Height);
                currentX += shift;
                var newPosition = new Vector3Int((int) currentX, (int) y, 10);
                AddTilesToTilemapChunks(chunk, newPosition
                );
                if (Random.value < 0.7f)
                    Instantiate(DecorationObject, Tilemap.CellToWorld(newPosition), Quaternion.identity, transform);
            }
        }
    }
}