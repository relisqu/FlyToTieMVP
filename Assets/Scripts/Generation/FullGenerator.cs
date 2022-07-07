using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Generation
{
    public class FullGenerator : MonoBehaviour
    {
        [SerializeField] private LevelGenerator IslandsGenerator;
        [SerializeField] private DecorationGenerator DecorGenerator;
        [SerializeField] private PropsGenerator PropsGenerator;

        [Button("Generate Level")]
        public void GenerateLevel()
        {
            IslandsGenerator.GenerateIslands();
            DecorGenerator.GenerateIslands();
            IslandsGenerator.GenerateLasers();
            PropsGenerator.GenerateUnits();

            StartCoroutine(GenerateBoxes());
        }

        public IEnumerator GenerateBoxes()
        {
            yield return new WaitForEndOfFrame();
            PropsGenerator.GenerateBoxes();
            PropsGenerator.GenerateEnemies();
        }
    }
}