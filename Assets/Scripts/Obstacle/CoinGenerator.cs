using DefaultNamespace.Props;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Obstacle
{
    public class CoinGenerator : MonoBehaviour
    {
        private static Coin[] _coinsPool = new Coin[30];
        [SerializeField] private Coin CoinPrefab;

        void Awake()
        {
            for (int i = 0; i < _coinsPool.Length; i++)
            {
                _coinsPool[i] = Instantiate(CoinPrefab);
                _coinsPool[i].gameObject.SetActive(false);
            }
        }

        public static void GenerateMoney(int amount, Transform transform)
        {
            for (int i = 0; i < amount; i++)
            {
                var randomOffset = new Vector2(Random.Range(-0.6f, 0.6f), Random.Range(-0.2f, 0.2f));
                var coin = GetCoinFromPool();
                var position = transform.position;
                coin.transform.position = position;
                coin.transform.localScale = Vector3.one * 0.3f;
                coin.transform.DOScale(Vector3.one, 0.3f);
                coin.gameObject.SetActive(true);
                coin.SetAlive(true);
                coin.transform.DOJump(position + (Vector3)randomOffset, 1f, 1, 0.3f);
            }
        }

        public static void ClearCoins()
        {
            for (int i = 0; i < _coinsPool.Length; i++)
            {
                _coinsPool[i].gameObject.SetActive(false);
            }
        }

        private static Coin GetCoinFromPool()
        {
            for (int i = 0; i < _coinsPool.Length; i++)
            {
                var coin = _coinsPool[i];
                if (!coin.isActiveAndEnabled) return coin;
            }

            return null;
        }
    }
}