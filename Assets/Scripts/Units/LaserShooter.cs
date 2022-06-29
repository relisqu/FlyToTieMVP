using UnityEngine;

namespace Units
{
    public class LaserShooter : Shooter
    {
        private Projectile _laser;

        public override void Shoot(bool isProjectileStatic)
        {
            if (_laser == null)
            {
                _laser = Instantiate(Projectile, transform);
            }

            _laser.SpawnProjectile();
        }
    }
}