using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class ObstacleDestructible : MonoBehaviour
    {
        [SerializeField] private bool UsesAnimation;

        [ShowIf("UsesAnimation")] [SerializeField]
        private Animator Animator;

        private static readonly int Damage = Animator.StringToHash("TakeDamage");

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Obstacle.Obstacle obstacle))
            {
                TakeDamage();
            }
        }

        private void TakeDamage()
        {
            if (UsesAnimation)
            {
                Animator.SetTrigger(Damage);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}