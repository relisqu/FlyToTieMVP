using System.Collections.Generic;
using Projectiles;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] protected Projectile Projectile;

    public virtual void Shoot()
    {
        Instantiate(Projectile, transform.position, Quaternion.identity, null);
    }

    public virtual void StopShooting()
    {
        
    }
}