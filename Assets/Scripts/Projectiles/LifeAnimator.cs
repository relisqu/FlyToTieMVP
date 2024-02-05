using UnityEngine;

namespace DefaultNamespace.Projectile
{
    public class LifeAnimator : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private ParticleSystem DeathParticles;
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

        public void PlayDeathAnimation()
        {
            Animator.SetTrigger(TakeDamage);
        }

        public void ShowDeathParticles()
        {
            
            DeathParticles.Play();
        }
    }
}