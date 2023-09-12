using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Generation
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData/Level", order = 0)]
    public class Level : SerializedScriptableObject
    {
        [DictionaryDrawerSettings(KeyLabel = "Level prefab", ValueLabel = "Priority" )]
        
        public Dictionary<LevelChunk, int> Chunks;
        
        [SerializeField] private int MinLevel = -1;
        [SerializeField] private int MaxLevel = int.MaxValue;
        public int GetMaxLevel => MaxLevel;
        public int GetMinLevel => MinLevel;
        [Button]
        void SetAutomaticLevelsRange()
        {
            var requirements = Chunks.Keys;
            foreach (var req in requirements)
            {
                MinLevel = Mathf.Max(req.GetMinLevel(), MinLevel);
                MaxLevel = Mathf.Min(req.GetMaxLevel(), MaxLevel);
            }
        }


    }
}