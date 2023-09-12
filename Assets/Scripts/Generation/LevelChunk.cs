using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Generation
{
    [InlineEditor(InlineEditorModes.LargePreview)]
    public class LevelChunk : MonoBehaviour
    {
        [SerializeField] [Min(0)]
        public float Width;

        [SerializeField] [Min(0)] private float Height;
        [SerializeField] public bool HasUnits;

        [SerializeField] [Min(1)] [ShowIf("HasUnits")]
        private int UnitsCount = 1;


        [SerializeField] private int MinLevel = -1;
        [SerializeField] private int MaxLevel = int.MaxValue;

        [Button]
        void SetAutomaticLevelsRange()
        {
            var requirements = GetComponentsInChildren<RequiresLevel>();
            foreach (var req in requirements)
            {
                MinLevel = Mathf.Max(req.GetMinLevel(), MinLevel);
                MaxLevel = Mathf.Min(req.GetMaxLevel(), MaxLevel);
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var localPosition = transform.position;

            Gizmos.DrawLine(localPosition + Vector3.left * Width / 2, localPosition + Vector3.right * Width / 2);
            Gizmos.DrawLine(localPosition + Vector3.up * Height / 2, localPosition + Vector3.down * Height / 2);
        }

        public int GetMinLevel()
        {
            return MinLevel;
        }

        public int GetMaxLevel()
        {
            return MaxLevel;
        }
    }
}