using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFollow : MonoBehaviour
    {
        [SerializeField] private PlayerMovement Player;
        [SerializeField] private float FollowSpeed;

        private void Start()
        {
        }

        private void LateUpdate()
        {
            var playerXPosition = Player.transform.position.x;
            var position = transform.position;
            if (playerXPosition > position.x)
            {
                position = Vector3.Lerp(position, new Vector3(playerXPosition, position.y, 0f),
                    FollowSpeed);
                transform.position = position;
            }
        }
    }
}