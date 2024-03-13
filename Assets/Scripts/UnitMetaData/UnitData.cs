using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "UnitUpgradeData", menuName = "ScriptableObjects/UnitMeta", order = 0)]
    public class UnitData : SerializedScriptableObject
    {
        public string SaveUnitString = "UnitLevel";
        public List<UnitLevelData> LevelsUpgrade;
        public String Name;
        public int FromLevel;
        public Unit Unit;


        public int LoadLevel()
        {
            return PlayerPrefs.GetInt(SaveUnitString, 0);
        }

        public void SaveLevel(int newLevel)
        {
            PlayerPrefs.SetInt(SaveUnitString, newLevel);
        }
    }

    [SerializeField]
    public class UnitLevelData
    {
        public string Description;
        public int UpgradeMoneyCount;
        public bool HasUpgrades;
        [ShowIf("HasUpgrades")] public UnitUpgrade Upgrade;
    }
}