using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFollow : MonoBehaviour
    {
        [SerializeField] private PlayerMovement Player;
        [SerializeField] private float FollowSpeed;
        [SerializeField] private Rigidbody2D Rigidbody2D;


        private void Update()
        {
            if (!_requiredToFollow) return;
            var playerXPosition = Player.transform.position.x;
            var position = transform.position;
            if (playerXPosition > position.x)
            {
                Rigidbody2D.position =  new Vector3(playerXPosition, position.y, 0f);
            }
        }

        public void ResetPosition()
        {
            Player.ResetPosition();
            var playerXPosition = Player.transform.position.x;
            Rigidbody2D.position = new Vector3(playerXPosition, transform.position.y, 0f);
        }

        public void StopFollowing()
        {
            _requiredToFollow = false;
        }

        public void Follow()
        {
            _requiredToFollow = true;
        }

        private bool _requiredToFollow = true;

        private void Awake()
        {
            Instance = this;
        }

        public static PlayerFollow Instance;
    }
}