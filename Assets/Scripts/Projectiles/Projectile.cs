using System;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float Speed = 1f;
        [SerializeField] protected float Scale = 1f;

        private float _defaultScale;
        public abstract void SpawnProjectile();


        public abstract void DestroyProjectile();

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public virtual void SetScale(float scale)
        {
            Scale = scale;
            Debug.Log($"Setted scale {Scale} {Time.time}");
            transform.localScale = Vector3.one * Scale;
        }

        public void SetDefaultScale(float scale)
        {
            _defaultScale = scale;
            ResetToDefaultScale();
        }

        public void ResetToDefaultScale()
        {
            Scale = _defaultScale;
        }

        private void OnEnable()
        {
           // transform.localScale = Vector3.one * Scale;
        }

    }
}