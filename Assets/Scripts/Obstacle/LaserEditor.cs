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

        private void OnDrawGizmos()
        {
            float offset = .0f;
            Collider.size = new Vector2(Collider.size.x, Renderer.size.y - 0.4f);
            BottomStars.transform.localPosition = new Vector2(0f, offset + Renderer.size.y/2);
            TopStars.transform.localPosition = new Vector2(0f, offset - Renderer.size.y/2);
        }
    }
}

