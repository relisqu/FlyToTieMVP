using System;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float Speed = 1f;
        [SerializeField] protected float Scale = 1f;
        
        public abstract void SpawnProjectile();


        public abstract void DestroyProjectile();

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
        
        public void SetScale(float scale)
        {
            Scale = scale;
            transform.localScale= Vector3.one*Scale;
        }
    }
}