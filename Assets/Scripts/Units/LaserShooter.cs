using System;
using Projectiles;
using UnityEngine;
using UnityEngine.UIElements;

namespace Units
{
    public class LaserShooter : Shooter
    {
        private LaserProjectile _laser;

        public override void Shoot()
        {

            shotsCount++;

            OnShoot?.Invoke(this);
            Projectile.SpawnProjectile();
        }

        private void Awake()
        {
            if (_laser == null)
            {
                _laser = Instantiate((LaserProjectile)Projectile, transform);
                Projectile = _laser;
            }
        }

        public override void SetProjectileScale(float scale)
        {
            _laser.SetScale(scale);
        }

        public override void StopShooting()
        {
            if (_laser != null)
                _laser.DestroyProjectile();
        }
        
    }
}