using System;
using System.Collections;
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
            if (other.gameObject.TryGetComponent(out Bullet bullet) &&
                (bullet.CanHit() || bullet.LastHitID != GetInstanceID()))
            {
                bullet.LastHitID = GetInstanceID();
                Debug.Log("Damaged by bullet");
                bullet.SetHit(false);
                TakeDamage();
                bullet.OnEnemyHit?.Invoke(bullet,other.GetContact(0).point);
                if (_currentHealth <= 0)
                {
                    Debug.Log("destroyed obj");
                    bullet.OnEnemyDestroy?.Invoke(other.GetContact(0).point);
                }

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