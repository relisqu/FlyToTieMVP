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
        [SerializeField] private int MaxHealth = 1;

        private static readonly int Die = Animator.StringToHash("TakeDamage");
        private static readonly int Damage = Animator.StringToHash("Hit");
        private int _currentHealth;

        private void Start()
        {
            _currentHealth = MaxHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet) && bullet.CanHit())
            {
                Debug.Log("Damaged by bullet");
                bullet.SetHit(false);
                TakeDamage();
                if (bullet.IsDestructible)
                {
                    bullet.TakeDamage();
                }
            }
        }

        public virtual void TakeDamage()
        {
            _currentHealth -= 1;

            if (_currentHealth <= 0)
            {
                PlayDeadAnimation();
            }
            else
            {
                if (UsesAnimation)
                {
                    Animator.SetTrigger(Damage);
                }
            }
        }

        public void PlayDeadAnimation()
        {
            if (UsesAnimation)
            {
                Animator.SetTrigger(Die);
            }
            else
            {
                gameObject.SetActive(false);
            }
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