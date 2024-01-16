using Scripts.Obstacle;
using UnityEngine;

namespace Obstacle
{
    public class СoinSpawner : MonoBehaviour
    {
        public int MinCoinCount;
        public int MaxCoinCount;
        
        public void GenerateCoins()
        {
            var moneyCount = Random.Range(MinCoinCount, MaxCoinCount);
            CoinGenerator.GenerateMoney(moneyCount, transform);
        }
    }
}