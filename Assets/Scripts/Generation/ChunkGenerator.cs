using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField] private List<TilemapChunk> TilemapChunks;

        public void InitiateChunks()
        {
            foreach (var chunk in TilemapChunks)
            {
                chunk.GenerateTiles();
                chunk.GetHeight();
            }
        }

        TilemapChunk GetSpecificTypeChunk(TilemapChunk.IslandType type)
        {
            var chunks = TilemapChunks.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (var chunk in chunks)
            {
                if (chunk.Type == type)
                {
                    return chunk;
                }
            }

            return null;
        }

        public List<TilemapChunk> GenerateOneCombination()
        {
            List<TilemapChunk> list = new List<TilemapChunk>();
            var randomCombinationStr = Combinations[Random.Range(0, Combinations.Length)];
            foreach (var island in randomCombinationStr)
            {
                switch (island)
                {
                    case 'L':
                        list.Add(GetSpecificTypeChunk(TilemapChunk.IslandType.Large));
                        break;
                    case 'S':
                        list.Add(GetSpecificTypeChunk(TilemapChunk.IslandType.Small));
                        break;
                    case 'M':
                        list.Add(GetSpecificTypeChunk(TilemapChunk.IslandType.Medium));
                        break;
                }
            }

            return list;
        }

        [SerializeField]private string[] Combinations =
        {
            "LS", "L", "SSS", "SM", "M", "SS"
        };
    }
}