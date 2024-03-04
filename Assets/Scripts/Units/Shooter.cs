using System;
using System.Collections.Generic;
using Projectiles;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] protected Projectile Projectile;
    public Action<Vector3> OnBulletEnemyHit;

    public virtual void Shoot()
    {
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
}