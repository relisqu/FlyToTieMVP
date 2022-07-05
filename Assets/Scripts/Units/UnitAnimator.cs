using UnityEngine;

namespace Units
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField]private Animator Animator;
        private static readonly int Jump1 = Animator.StringToHash("Jump");
        private static readonly int Damage = Animator.StringToHash("TakeDamage");
        private bool _isDead;

        public void Jump()
        {
            if(!_isDead)Animator.SetTrigger(Jump1);
        }

        public void TakeDamage()
        {
            _isDead = true;
            Animator.SetTrigger(Damage);
        }

        public void DestroyObject()
        {
            print("Destroyed: "+gameObject);
            Destroy(gameObject);
        }
    }
}