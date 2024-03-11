using System;
using System.Collections;
using DG.Tweening;
using MoreMountains.Tools;
using Obstacle;
using Player;
using Projectiles;
using Scripts.Obstacle;
using UnityEngine;

namespace Units
{
    public class FastShooter : Shooter
    {
        [SerializeField] private int ProjectileCount;
        [SerializeField] private float Delay;

        private Coroutine _shooterCoroutine;
        private bool _isShooting;

        private IEnumerator ShootNProjectiles(int n)
        {
            _isShooting = true;
            for (int i = 0; i < n; i++)
            {
                AudioManager.instance.Play("fast_shot");
                var bullet = BulletPool.GetBulletFromPool();
                bullet.gameObject.SetActive(true);
                bullet.transform.position = transform.position;
                bullet.SetSpeed(_projectileSpeed);
                bullet.transform.localScale = new Vector3(0f, _projectileScale, _projectileScale);
                bullet.SpawnProjectile();
                bullet.GetBullet().OnEnemyDestroy+=OnBulletEnemyDeath;
                bullet.transform.DOScaleX(_projectileScale, 0.3f);
                yield return new WaitForSeconds(Delay);
            }

            _isShooting = false;
        }

        private float _projectileSpeed = 30f;
        private float _projectileScale = 1f;


        public override void SetProjectileScale(float scale)
        {
            _projectileScale = scale;
        }

        public override void SetProjectileSpeed(float speed)
        {
            _projectileSpeed = speed;
        }

        public override void Shoot()
        {
            if (!_isShooting)
                _shooterCoroutine = StartCoroutine(ShootNProjectiles(ProjectileCount));
        }

        public override void StopShooting()
        {
            if (_shooterCoroutine != null)
            {
                StopCoroutine(_shooterCoroutine);
            }

            _isShooting = false;
        }
    }
}