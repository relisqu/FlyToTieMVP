using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] protected Projectile Projectile;
    public float delay;

    public virtual void Shoot(bool isProjectileStatic)
    {
        if (Projectile != null)
        {
            Transform parentTransform = null;
            if (isProjectileStatic)
            {
                parentTransform = transform;
            }

            Instantiate(Projectile, transform.position, Quaternion.identity, parentTransform);
        }
    }
}