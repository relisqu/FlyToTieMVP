using Projectiles;
using UnityEngine;

namespace Units
{
    public class LaserShooter : Shooter
    {
        private Projectile _laser;

        public override void Shoot()
        {

            if (_laser == null)
            {
                _laser = Instantiate(Projectile, transform);
            }

            _laser.SpawnProjectile();
        }

        public override void StopShooting()
        {
            _laser.DestroyProjectile();
        }
    }
}