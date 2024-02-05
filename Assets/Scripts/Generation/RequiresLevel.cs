using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Generation
{
    public class RequiresLevel : MonoBehaviour
    {
        [SerializeField] private bool HasMinLevel;

        [SerializeField] [ShowIf("HasMinLevel")]
        private int MinLevel;

        [SerializeField] private bool HasMaxLevel;

        [SerializeField] [ShowIf("HasMaxLevel")]
        private int MaxLevel;


        public int GetMinLevel()
        {
            if (HasMinLevel) return MinLevel;
            return -1;
        }

        public int GetMaxLevel()
        {
            if (HasMaxLevel) return MaxLevel;
            return int.MaxValue;
        }
    }
}