using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UI;
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
        [SerializeField] private float CheckDistance = 1.2f;
        [SerializeField] private LayerMask ObstacleLayerMask;

        public void TakeDamage()
        {
            if (StarterUnit.IsInvincible()) return;
            //StopAllCoroutines();
            ScreenBlink.Instance.Blink();

            CameraShake.ShakeCamera(0.3f, 5f);
            PlayerMovement.SetState(PlayerMovement.MovementState.TakeDamage);
            var direction = CalculateDirection(transform.position, JumpDirection);
            var currentJumpForce = JumpForce;
            if (direction.y < 0)
            {
                currentJumpForce *= 1.5f;
            }

            transform.DOLocalJump(transform.position + (Vector3)direction, currentJumpForce, 1, JumpDuration)
                .OnComplete(() => { PlayerMovement.SetState(PlayerMovement.MovementState.Move); });

            StartCoroutine(SetInvisible());
        }


        private void OnDrawGizmos()
        {
            var position = transform.position;
            var defDirection = new Vector2(JumpDirection.x, JumpDirection.y);


            Vector3 point1 = position + (Vector3)defDirection;
            Vector3 point2 = position + Vector3.right * defDirection.x * 1.5f;
            Vector3 point3 = position + new Vector3(defDirection.x, -defDirection.y, 0);
            Gizmos.color = Color.magenta;

            Gizmos.DrawLine(position, point1);
            Gizmos.DrawLine(position, point2);
            Gizmos.DrawLine(position, point3);

            Vector3 minPoint = point1;
            var pointList = new List<Vector3>();
            pointList.Add(point1);
            pointList.Add(point3);

            foreach (var point in pointList)
            {
                var min = GetObstacle(minPoint);
                var cur = GetObstacle(point);
                if (min.distance <= cur.distance)
                {
                    minPoint = point;
                }

                Gizmos.DrawRay(point, Vector2.right * CheckDistance);
            }

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(minPoint, 0.3f);
        }

        private Vector2 CalculateDirection(Vector3 position, Vector3 defDirection)
        {
            defDirection = new Vector2(JumpDirection.x, JumpDirection.y);
            Vector3 point1 = position + defDirection;
            Vector3 point3 = position + new Vector3(defDirection.x, -defDirection.y, 1);

            var pointList = new List<Vector3>();

            pointList.Add(point1);
            pointList.Add(point3);


            Vector3 minPoint = point1;
            foreach (var point in pointList)
            {
                var min = GetObstacle(minPoint);
                var cur = GetObstacle(point);
                if (min.distance < cur.distance)
                {
                    minPoint = point;
                }
            }

            return minPoint - position;
        }

        private RaycastHit2D GetObstacle(Vector3 point)
        {
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.right, CheckDistance, ObstacleLayerMask);


            if (hit.collider != null && hit.transform.TryGetComponent(out Obstacle _))
            {
                return hit;
            }

            var miss = new RaycastHit2D();
            miss.distance = 10000;
            return miss;
        }


        private IEnumerator SetInvisible()
        {
            StarterUnit.SetInvincible(true);
            yield return new WaitForSeconds(ImmuneDuration);
            StarterUnit.SetInvincible(false);
        }
    }
}