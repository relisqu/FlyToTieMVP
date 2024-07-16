using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public abstract class UnitUpgrade : SerializedScriptableObject
    {
        public abstract void UpgradeAction(Unit unit);
        public abstract void ClearUpgradeActions(Unit unit);
    }
}