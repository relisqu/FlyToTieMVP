﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Obstacle
{
    public class Narval : MonoBehaviour
    {
        [SerializeField] private float TriggerDistance;
        [SerializeField] private Animator Animator;
        private Transform _goalTransform;

        private void Update()
        {
            if (!_isAwake && Vector3.Distance(_goalTransform.position, transform.position) <= TriggerDistance)
            {
                WakeUp();
            }
        }

        void WakeUp()
        {
            _isAwake = true;
            Animator.SetTrigger(WakeUpIdx);
        }

        public void GenerateCoins()
        {
            var moneyCount = Random.Range(1, 5);
            CoinGenerator.GenerateMoney(moneyCount, transform);
        }

        private bool _isAlive;

        private void Start()
        {
            _isAlive = true;
            _goalTransform = StarterUnit.Instance.transform;
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            Gizmos.DrawLine(position, position + TriggerDistance * Vector3.left);
        }

        private bool _isAwake;
        private static readonly int WakeUpIdx = Animator.StringToHash("WakeUp");
    }
}