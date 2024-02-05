using UnityEngine;

namespace Projectiles
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private MovingProjectile Projectile;
        public static MovingProjectile[] ProjectilesPool;

        private void Start()
        {
            ProjectilesPool = new MovingProjectile[50];
            for (int i = 0; i < ProjectilesPool.Length; i++)
            {
                ProjectilesPool[i] = Instantiate(Projectile);
                ProjectilesPool[i].gameObject.SetActive(false);
            }
        }

        public static MovingProjectile GetBulletFromPool()
        {
            foreach (var bullet in ProjectilesPool)
            {
                if (!bullet.isActiveAndEnabled)
                {
                    return bullet;
                }
            }

            return null;
        }
    }
}