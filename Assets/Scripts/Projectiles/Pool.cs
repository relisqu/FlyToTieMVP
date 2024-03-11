using UnityEngine;

namespace Projectiles
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private GameObject Projectile;
        public static GameObject[] ParticlesPool;

        private void Start()
        {
            ParticlesPool = new GameObject[15];
            for (int i = 0; i < ParticlesPool.Length; i++)
            {
                ParticlesPool[i] = Instantiate(Projectile);
                ParticlesPool[i].SetActive(false);
            }
        }

        public static GameObject GetBulletFromPool()
        {
            foreach (var ob in ParticlesPool)
            {
                if (!ob.activeInHierarchy)
                {
                    return ob;
                }
            }

            return null;
        }
    }
}