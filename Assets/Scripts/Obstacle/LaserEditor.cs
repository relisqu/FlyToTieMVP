using System;
using UnityEngine;

namespace DefaultNamespace.Obstacle
{
    public class LaserEditor : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D Collider;
        [SerializeField] private SpriteRenderer Renderer;
        [SerializeField] private Transform BottomStars;
        [SerializeField] private Transform TopStars;
        [SerializeField] private ParticleSystem ParticleSystem;

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                RedrawLaser();
            }
        }

        public void ChangeRendererYSize(float newSize)
        {
            Renderer.size = new Vector2(Renderer.size.x, newSize);
            RedrawLaser();
        }

        public void RedrawLaser()
        {
            float offset = .0f;
            var size = Renderer.size;
            Collider.size = new Vector2(Collider.size.x, size.y - 0.1f);
            BottomStars.transform.localPosition = new Vector2(0f, offset + size.y / 2);
            TopStars.transform.localPosition = new Vector2(0f, offset - size.y / 2);
            var particleSystemShape = ParticleSystem.shape;
            particleSystemShape.scale = new Vector3(size.y, 1f, 1f);
        }
    }
}