using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class BulletDestructible : MonoBehaviour
    {
        [SerializeField] private bool UsesAnimation;

        [ShowIf("UsesAnimation")] [SerializeField]
        private Animator Animator;

        private static readonly int Damage = Animator.StringToHash("TakeDamage");

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
            if (UsesAnimation)
            {
                Animator.SetTrigger(Damage);
                return;
            }

            Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}