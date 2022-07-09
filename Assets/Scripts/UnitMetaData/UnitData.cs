using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class UnitData : MonoBehaviour
    {
        public bool IsVisited;
        public int FinishedTimes;
        public Unit DataUnit;
        public Action Upgraded;

        public virtual void UpgradeFirstTime()
        {
        }
        
        
        public virtual void UpgradeSecondTime()
        {
        }
        
        
        public virtual void UpgradeThirdTime()
        {
        }
    }
}