﻿using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Obstacle;
using Player;
using Sirenix.OdinInspector;
using Subtegral.WeightedRandom;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class LevelGenerator : MonoBehaviour
    {
        [BoxGroup("Level constrains")] [SerializeField]
        private float LevelWidth;


        [BoxGroup("Level constrains")] [SerializeField]
        private int MinSpaceBetweenUnitChunks;

        [SerializeField] private LevelCollection levels;

        private float _minSize = 5;

        public static LevelGenerator Instance;
        [SerializeField] private Transform LevelEnd;

        private void Awake()
        {
            Instance = this;
        }

        public Level GetLevel()
        {
            int curLevel = PlayerData.СurrentLevel;
            if (levels.Levels.Count < curLevel)
            {
                int trysCount = 150;
                while (trysCount > 0)
                {
                    var level = levels.Levels[Random.Range(0, levels.Levels.Count)];
                    if (level.GetMaxLevel >= curLevel && curLevel >= level.GetMinLevel)
                    {
                        return level;
                    }

                    trysCount--;
                }

                return levels.Levels[Random.Range(0, levels.Levels.Count)];
            }

            return levels.Levels[curLevel - 1];
        }

        public List<LevelChunk> GenerateLevelChunks()
        {
            var level = GetLevel();
            WeightedRandom<LevelChunk> random = new WeightedRandom<LevelChunk>();
            foreach (var chunk in level.Chunks)
            {
                random.Add(chunk.Key, chunk.Value);
            }

            float currentWidth = 0;
            var ind = 0;
            var indWithUnit = 0;
            List<LevelChunk> chunksToGenerate = new List<LevelChunk>();
            while (currentWidth <= LevelWidth)
            {
                var chunk = random.Next();
                if (chunk == chunksToGenerate.LastOrDefault()) continue;
                ind++;
                if (chunk.HasUnits)
                {
                    if (ind - indWithUnit > MinSpaceBetweenUnitChunks)
                    {
                        chunksToGenerate.Add(chunk);
                        indWithUnit = ind;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    chunksToGenerate.Add(chunk);
                }


                currentWidth += chunk.Width;
            }

            return chunksToGenerate;
        }

        [Button]
        public void SpawnLevel()
        {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            var chunks = GenerateLevelChunks();
            var curWidth = 0f;
            var finalXPos = 0f;
            foreach (var chunk in chunks)
            {
                curWidth += chunk.Width / 2;
                var chunkObj = Instantiate(chunk, transform);
                chunkObj.transform.localPosition = Vector3.right * curWidth;
                curWidth += chunk.Width / 2;
                finalXPos = chunkObj.transform.position.x + chunk.Width / 2;
            }

            LevelEnd.transform.position = Vector3.right * finalXPos;
        }
    }
}