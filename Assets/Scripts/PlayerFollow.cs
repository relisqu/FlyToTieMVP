using System;
using Cinemachine;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFollow : MonoBehaviour
    {
        [SerializeField] private PlayerMovement Player;
        public CinemachineVirtualCamera СinemachineVirtualCamera;

        private void Start()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void LateUpdate()
        {
            if (!_requiredToFollow) return;
        }

        public void ResetPosition()
        {
            Player.ResetPosition();
            СinemachineVirtualCamera.Follow = Player.transform;
        }

        public void StopFollowing()
        {
            СinemachineVirtualCamera.Follow =  null;
            _requiredToFollow = false;
        }

        public void Follow()
        {
            СinemachineVirtualCamera.Follow =  Player.transform;
            _requiredToFollow = true;
        }

        private bool _requiredToFollow = true;

        private void Awake()
        {
            Instance = this;
        }

        public static PlayerFollow Instance;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
    }
}