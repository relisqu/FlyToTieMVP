using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class JumpSplashUI : MonoBehaviour
    {
        public List<ParticleSystem> Particles;
        private Camera _camera;
        public static JumpSplashUI Instance;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(Particles.Count<4) return;
            
            Particles[0].transform.position= _camera.ViewportToWorldPoint(new Vector2(0,0));
            Particles[1].transform.position= _camera.ViewportToWorldPoint(new Vector2(0,1));
            Particles[1].transform.rotation = Quaternion.Euler(0,0,-90f);
            Particles[3].transform.position= _camera.ViewportToWorldPoint(new Vector2(1,1));
            Particles[3].transform.rotation = Quaternion.Euler(0,0,-180f);
            Particles[2].transform.position= _camera.ViewportToWorldPoint(new Vector2(1,0));
            Particles[2].transform.rotation = Quaternion.Euler(0,0,-270f);
        }

        public void PlayParticles()
        {
            foreach (var p in Particles)
            {
                p.Play();
            }
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}