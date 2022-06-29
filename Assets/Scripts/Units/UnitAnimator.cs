using UnityEngine;

namespace Units
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField]private Animator Animator;
        private static readonly int Jump1 = Animator.StringToHash("Jump");
        private static readonly int Damage = Animator.StringToHash("TakeDamage");

        public void Jump()
        {
            Animator.SetTrigger(Jump1);
        }

        public void TakeDamage()
        {
            Animator.SetTrigger(Damage);
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}