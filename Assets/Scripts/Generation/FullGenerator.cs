using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Generation
{
    public class FullGenerator : MonoBehaviour
    {
        [SerializeField]private LevelGenerator IslandsGenerator;
        [SerializeField]private DecorationGenerator DecorGenerator;

        [Button("Generate Level")]
        public void GenerateLevel()
        {
            IslandsGenerator.GenerateIslands();
            DecorGenerator.GenerateIslands();
            IslandsGenerator.GenerateLasers();
        }
    }
}