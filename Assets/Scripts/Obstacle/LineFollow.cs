using System;
using DG.Tweening;
using UnityEngine;

namespace Obstacle
{
    public class LineFollow : MonoBehaviour
    {
        [SerializeField] private LineRenderer LineRenderer;
        [SerializeField] private float Speed;
        [SerializeField] private Ease MovementEasing;
        [SerializeField] private bool HasPingPongMovement = true;

        private int _curPoint;

        private void Start()
        {
            transform.localPosition = Vector3.Scale(LineRenderer.GetPosition(0), LineRenderer.transform.localScale) +
                                      LineRenderer.transform.localPosition;

            MoveToPoint(0);
        }

        private bool hasForwardDir = true;

        void MoveToPoint(int i)
        {
            var nextInd = i;

            if (!HasPingPongMovement)
            {
                nextInd++;
                nextInd %= LineRenderer.positionCount - 1;
                MoveToPointByIndex(nextInd);
                return;
            }

            if (i <= 0)
            {
                hasForwardDir = true;
            }

            if (i >= LineRenderer.positionCount - 1)
            {
                hasForwardDir = false;
            }

            if (hasForwardDir)
            {
                nextInd++;
            }
            else
            {
                nextInd--;
            }

            MoveToPointByIndex(nextInd);
        }

        public void MoveToPointByIndex(int index)
        {
            transform.DOLocalMove(
                    Vector3.Scale(LineRenderer.GetPosition(index), LineRenderer.transform.localScale) +
                    LineRenderer.transform.localPosition,
                    Speed).SetSpeedBased()
                .SetEase(MovementEasing).OnComplete(
                    () => { MoveToPoint(index); });
        }

    }
}