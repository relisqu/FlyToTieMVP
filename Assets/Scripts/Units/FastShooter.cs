using System.Collections;
using MoreMountains.Tools;
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
                Instantiate(Projectile, transform.position, Quaternion.identity, null);
                Projectile.SpawnProjectile();
                yield return new WaitForSeconds(Delay);
            }
            _isShooting = false;
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