using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Obstacle
{
    public class FollowingEnemy : MonoBehaviour
    {
        [SerializeField] private float FollowDistance;
        [SerializeField] private float FollowSpeed;
        [SerializeField] private LayerMask Layers;
        private bool _isAwake;
        private Transform _player;

        private void Start()
        {
            _isAwake = false;
            _player = StarterUnit.Instance.transform;
        }

        private void Update()
        {
            var pl = _player.position;
            var transf = transform.position;
            if (Vector2.Distance(pl, transf) < FollowDistance)
            {
                var hit = Physics2D.Raycast(transf, pl - transf, Mathf.Infinity, Layers);
                if (hit && hit.transform.TryGetComponent(out Unit unit))
                {
                    Move();
                }
            }
        }

        public void Move()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _player.position, FollowSpeed * Time.deltaTime);
        }
    }
}