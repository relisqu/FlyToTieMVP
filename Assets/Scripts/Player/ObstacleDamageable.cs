using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class ObstacleDamageable : MonoBehaviour
    {
        [SerializeField] private float ImmuneDuration;
        [SerializeField] private Vector2 JumpDirection;
        [SerializeField] private float JumpForce;
        [SerializeField] private float JumpDuration;
        [SerializeField] private PlayerMovement PlayerMovement;

        public void TakeDamage()
        {
            print("AAA");
            PlayerMovement.SetState(PlayerMovement.MovementState.TakeDamage);
            transform.DOLocalJump(transform.position + (Vector3) JumpDirection, JumpForce, 1, JumpDuration)
                .OnComplete(() => { PlayerMovement.SetState(PlayerMovement.MovementState.Move); });

            StartCoroutine(SetInvisible());
        }

        private IEnumerator SetInvisible()
        {
            StarterUnit.SetInvisible(true);
            yield return new WaitForSeconds(ImmuneDuration);
            StarterUnit.SetInvisible(false);
        }
    }
}