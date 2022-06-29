using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] protected Projectile Projectile;
    public float delay;

    public virtual void Shoot()
    {
        Instantiate(Projectile, transform.position, Quaternion.identity, null);
    }

    public virtual void StopShooting()
    {
        
    }
}