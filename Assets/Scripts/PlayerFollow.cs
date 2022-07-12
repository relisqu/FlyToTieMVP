using System;
using MoreMountains.Feedbacks;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFollow : MonoBehaviour
    {
        [SerializeField] private PlayerMovement Player;
        [SerializeField] private float FollowSpeed;


        private void LateUpdate()
        {
            var playerXPosition = Player.transform.position.x;
            var position = transform.position;
            if (_requiredToFollow && playerXPosition > position.x)
            {
                position = Vector3.Lerp(position, new Vector3(playerXPosition, position.y, 0f),
                    FollowSpeed);
                transform.position = position;
            }
        }

        public void ResetPosition()
        {
            Player.ResetPosition();
            var playerXPosition = Player.transform.position.x;
            transform.position = new Vector3(playerXPosition, transform.position.y, 0f);
        }

        public void StopFollowing()
        {
            _requiredToFollow = false;
        }

        public void Follow()
        {
            _requiredToFollow = true;
        }

        private bool _requiredToFollow=true;
        private void Awake()
        {
            Instance = this;
        }

        public static PlayerFollow Instance;
    }
}