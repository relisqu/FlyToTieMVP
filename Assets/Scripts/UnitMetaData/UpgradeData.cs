using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "UnitUpgrade", menuName = "ScriptableObjects/UnitUpgrades", order = 0)]
    public class UpgradeData : ScriptableObject
    {
        private string TitleText;
        private string LockedText;
        private string[] UpgradedTexts;
        private int[] UpgradedCosts;
    }
}