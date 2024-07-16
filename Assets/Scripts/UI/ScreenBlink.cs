using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class ScreenBlink : MonoBehaviour
    {
        public static ScreenBlink Instance;

        private Animator Animator;

        private void Start()
        {
            Animator = GetComponent<Animator>();
            Instance = this;
        }

        public void Blink()
        {
            Animator.SetTrigger("Blink");
        }
        public void SmallBlink()
        {
            Animator.SetTrigger("SmallBlink");
        }
    }
}