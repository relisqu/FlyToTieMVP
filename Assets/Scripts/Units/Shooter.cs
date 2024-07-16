using System;
using System.Collections.Generic;
using Projectiles;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] protected Projectile Projectile;
    public Action<Vector3> OnBulletEnemyDeath;
    public Action<Shooter> OnShoot;


    protected int shotsCount = 0;

    public Action<Bullet, Vector3> OnBulletHit;

    public int GetShotsCount() => shotsCount;

    public virtual void Shoot()
    {
        OnShoot?.Invoke(this);
        shotsCount++;
        var bullet = Instantiate(Projectile, transform.position, Quaternion.identity, null);
    }

    public virtual void SetProjectileSpeed(float speed)
    {
        Projectile.SetSpeed(speed);
    }


    public virtual void SetProjectileScale(float scale)
    {
        Projectile.SetScale(scale);
    }

    public virtual void StopShooting()
    {
    }


    public virtual void ResetProjectileScale()
    {
        Projectile.ResetToDefaultScale();
    }

    public virtual void SetDefaultProjectileScale(float laserScale)
    {
        Projectile.SetDefaultScale(laserScale);
    }
}