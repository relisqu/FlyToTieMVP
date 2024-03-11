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
            Projectile.GetBullet().OnEnemyHit += CallBulletHit;
        }

        void CallBulletHit(Bullet bull, Vector3 pos)
        {
            OnBulletHit?.Invoke(bull, pos);
        }

        private void OnDestroy()
        {
            Projectile.GetBullet().OnEnemyHit -= CallBulletHit;
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