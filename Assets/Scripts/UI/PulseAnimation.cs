using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class PulseAnimation : MonoBehaviour
    {
        [SerializeField]private float Speed=0.4f;
        [SerializeField]private float Force=0.7f;
        private void Start()
        {

            transform.DOScale(Force, Speed).SetLoops(-1, LoopType.Yoyo);
        }
    }
}