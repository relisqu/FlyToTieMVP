using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class PulseAnimation : MonoBehaviour
    {
        private void Start()
        {

            transform.DOScale(0.7f, 0.4f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}