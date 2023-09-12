using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Generation
{
    [CreateAssetMenu(fileName = "LevelCollection", menuName = "ScriptableObjects/LevelData/LevelCollection", order = 1)]
    public class LevelCollection : ScriptableObject
    {
        public List<Level> Levels;

    }
}