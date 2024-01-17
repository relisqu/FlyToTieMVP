using Scripts.Obstacle;
using UnityEngine;

namespace Obstacle
{
    public class СoinSpawner : MonoBehaviour
    {
        public int MinCoinCount;
        public int MaxCoinCount;
        public float GenerationChance = 1f;

        public void GenerateCoins()
        {
            if (Random.value >= GenerationChance) return;

            var moneyCount = Random.Range(MinCoinCount, MaxCoinCount);
            CoinGenerator.GenerateMoney(moneyCount, transform);
        }
    }
}