using System;
using Projectiles;
using UnityEngine;
using UnityEngine.UIElements;

namespace Units
{
    public class LaserShooter : Shooter
    {
        public override void Shoot()
        {
            shotsCount++;

            OnShoot?.Invoke(this);
            Projectile.SpawnProjectile();
        }

        private void Awake()
        {
            Projectile = Instantiate((LaserProjectile)Projectile, transform);
        }

        public override void SetProjectileScale(float scale)
        {
            ((LaserProjectile)Projectile).SetScale(scale);
        }

        public override void StopShooting()
        {
            
        }
    }
}