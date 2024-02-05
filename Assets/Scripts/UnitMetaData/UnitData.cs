using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName ="UnitUpgradeData", menuName = "ScriptableObjects/UnitMeta", order = 0)]
    public class UnitData : ScriptableObject
    {
        public string SaveUnitString = "MainUnitLevel";
        public List<UnitLevelData> LevelsUpgrade;
    }

    public class UnitLevelData
    {
        public int FromLevel;
        public string Description;
        public int UpgradeCount;
    }
}