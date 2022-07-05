using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Obstacle
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private Animator LaserAnimator;
        [SerializeField] private float OffDuration;
        [SerializeField] private List<SpriteRenderer> FirstDots;
        [SerializeField] private List<SpriteRenderer> SecondDots;
        [SerializeField] private float OnDuration;
        [SerializeField] private ParticleSystem Particles;
        private bool _isEnabled;
        private float _currentStateTime;
        private static readonly int TurnOff = Animator.StringToHash("TurnOff");
        private static readonly int TurnOn = Animator.StringToHash("TurnOn");


        public void TurnOnDots(List<SpriteRenderer> dots)
        {
            foreach (var dot in dots)
            {
                dot.DOColor(Color.white,0.2f);
            }
        }

        public void TurnOffDots(List<SpriteRenderer> dots)
        {
            foreach (var dot in dots)
            {
                dot.DOColor(Color.clear,0.2f);
            }
        }
        private IEnumerator ChangeState()
        {
            while (true)
            {
                TurnOnDots(FirstDots);
                yield return new WaitForSeconds(OffDuration/2);
                TurnOnDots(SecondDots);
                yield return new WaitForSeconds(OffDuration/2);
                LaserAnimator.SetTrigger(TurnOn);
                TurnOffDots(FirstDots);
                TurnOffDots(SecondDots);
                Particles.Play();
                Particles.Emit(15);
                yield return new WaitForSeconds(OnDuration);
                Particles.Stop();
                LaserAnimator.SetTrigger(TurnOff);
            }
        }

        private void Start()
        {
            TurnOffDots(FirstDots);
            TurnOffDots(SecondDots);
            Particles.Stop();
            StartCoroutine(ChangeState());
        }
    }
}