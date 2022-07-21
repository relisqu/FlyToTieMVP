using System;
using DefaultNamespace.UI;
using UnityEngine;

namespace Units
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        private static readonly int Jump1 = Animator.StringToHash("Jump");
        private static readonly int Damage = Animator.StringToHash("TakeDamage");
        private bool _isDead;

        public void Jump()
        {
            if (!_isDead) Animator.SetTrigger(Jump1);
        }

        public void TakeDamage()
        {
            if (Cutscene.IsPlayingCutscene) return;
            _isDead = true;
            Animator.SetTrigger(Damage);
        }

        private void OnEnable()
        {
            _isDead = false;
        }

        public void DestroyObject()
        {
            print("Destroyed: " + gameObject);
            gameObject.SetActive(false);
        }

        public void SetTag(string idle)
        {
            Animator.SetTrigger(idle);
        }
    }
}