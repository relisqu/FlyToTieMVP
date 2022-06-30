using System;
using UnityEngine;

namespace Player
{
    public class BulletDestructible : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet))
            {
                TakeDamage();
                if (bullet.IsDestructible)
                {
                    bullet.TakeDamage();
                }
            }
        }

        public virtual void TakeDamage()
        {
            Destroy(gameObject);
        }
    }
}