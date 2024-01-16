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

        private int _curPoint;

        private void Start()
        {
            transform.position = LineRenderer.GetPosition(0)+LineRenderer.transform.position;
            MoveToPoint(0);
        }

        private bool hasForwardDir = true;

        void MoveToPoint(int i)
        {
            var nextInd = i;

            if (i <= 0)
            {
                hasForwardDir = true;
            }

            if (i >= LineRenderer.positionCount-1)
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

            transform.DOMove(LineRenderer.GetPosition(nextInd)+LineRenderer.transform.position, Speed).SetSpeedBased().SetEase(MovementEasing).OnComplete(
                () =>
                {
                    MoveToPoint(nextInd);
                });
        }

        private void Update()
        {
        }
    }
}