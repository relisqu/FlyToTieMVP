using UnityEngine;

namespace Projectiles
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private ParticleSystem Particle;
        public static ParticleSystem[] ParticlesPool;

        private void Start()
        {
            ParticlesPool = new ParticleSystem[15];
            for (int i = 0; i < ParticlesPool.Length; i++)
            {
                ParticlesPool[i] = Instantiate(Particle);
                ParticlesPool[i].gameObject.SetActive(false);
            }
        }

        public static ParticleSystem GetParticleFromPool()
        {
            foreach (var ob in ParticlesPool)
            {
                if (!ob.gameObject.activeInHierarchy)
                {
                    return ob;
                }
            }

            return null;
        }
    }
}