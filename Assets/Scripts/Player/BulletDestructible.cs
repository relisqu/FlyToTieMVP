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

        [SerializeField] public string DeathSoundTag;
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
                PlayDeathSound();
                return;
            }

            gameObject.SetActive(false);
        }

        public void PlayDeathSound()
        {
            if (DeathSoundTag != null)
                AudioManager.instance.Play(DeathSoundTag);
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}