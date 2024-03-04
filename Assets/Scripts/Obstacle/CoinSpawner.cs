using System;
using DefaultNamespace.Props;
using Scripts.Obstacle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacle
{
    public class CoinSpawner : MonoBehaviour
    {
        public int MinCoinCount;
        public int MaxCoinCount;
        public float GenerationChance = 1f;


        public void GenerateCoins()
        {
            if (Random.value >= GenerationChance) return;

            var moneyCount = Random.Range(MinCoinCount, MaxCoinCount);
            CoinGenerator.GenerateMoney(moneyCount, transform.position);
        }

        public void GenerateCoinsAtPoint(Vector3 position)
        {
            if (Random.value >= GenerationChance) return;

            var moneyCount = Random.Range(MinCoinCount, MaxCoinCount);
            CoinGenerator.GenerateMoney(moneyCount, position);
        }

        public static void GenerateCoinsAtPoint(int MinCoinCount, int MaxCoinCount, float GenerationChance, Vector3 position)
        {
            if (Random.value >= GenerationChance) return;

            var moneyCount = Random.Range(MinCoinCount, MaxCoinCount);
            CoinGenerator.GenerateMoney(moneyCount, position);
        }
    }
}