using System.Collections;
using DefaultNamespace;
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
            if(StarterUnit.IsInvincible()) return;
            //StopAllCoroutines();
            CameraShake.ShakeCamera(0.3f,5f);
            PlayerMovement.SetState(PlayerMovement.MovementState.TakeDamage);
            transform.DOLocalJump(transform.position + (Vector3) JumpDirection, JumpForce, 1, JumpDuration)
                .OnComplete(() => { PlayerMovement.SetState(PlayerMovement.MovementState.Move); });

            StartCoroutine(SetInvisible());
        }

        private IEnumerator SetInvisible()
        {
            StarterUnit.SetInvincible(true);
            yield return new WaitForSeconds(ImmuneDuration);
            StarterUnit.SetInvincible(false);
        }
    }
}