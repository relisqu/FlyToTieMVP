using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Generation;
using Player;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class EndCutscene : Cutscene
    {
        public static Action OnGameplayFinish;
        public static bool IsPlayingEndCutscene;
        [SerializeField] private float TransformYPosition;
        [SerializeField] public List<CanvasGroup> GiftScenes;

        public override void AdditionalCutsceneStart()
        {
            IsPlayingEndCutscene = true;
            LockedPlayerInputMovement = true;
        }

        public override void AdditionalCutsceneStop()
        {
            IsPlayingEndCutscene = false;
            LockedPlayerInputMovement = false;
        }

        private void Update()
        {
            var playerPos = PlayerMovement.transform.position.x;
            var screenPos = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

            if (_canJump && IsPlayingEndCutscene && PlayerMovement.transform.position.y < TransformYPosition)
            {
                StartCoroutine(Jump());
                PlayerMovement.Jump();
            }
            else if (PlayerMovement.transform.position.y < TransformYPosition - 0.3f&& IsPlayingEndCutscene)
            {
                PlayerMovement.Jump();
            }
        }

        public void DestroyMoneyText()
        {
            StartCoroutine(RemoveMoneyText());
        }

        public IEnumerator RemoveMoneyText()
        {
            // yield return StartCoroutine(StartCutscene.DisableScenes(StartCutscene.DisabledUIScenes));
            DisableScenes(DisabledUIScenes);

            if ((PlayerData.СurrentLevel - 1) % 4 == 0)
            {
                EnableScenes(GiftScenes);
            }
            else
            {
                yield return StartCoroutine(FinishScene());
            }
        }

        IEnumerator FinishScene()
        {
            DisableScenes(GiftScenes);
            PlayerFollow.Instance.StopFollowing();

            LevelGenerator.Instance.SpawnLevel();
            var playerPos = PlayerMovement.transform.position.x;
            var screenPos = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

            var distance = screenPos - playerPos;
            var speed = distance / 2.5f;
            while (playerPos < screenPos + 0.2f)
            {
                PlayerMovement.SetSpeed(speed);
                playerPos = PlayerMovement.transform.position.x;
                screenPos = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
                yield return null;
            }

            StarterUnit.Instance.RemoveAllChildren();
            IsPlayingEndCutscene = false;
            LockedPlayerInputMovement = false;
            FinishedScene?.Invoke();
            OnGameplayFinish?.Invoke();

            gameObject.SetActive(false);
        }

        public static Action FinishedScene;

        private void Start()
        {
            _camera = Camera.main;
            Gift.OpenedGift += FinishCutscene;
        }

        private void OnDestroy()
        {
            Gift.OpenedGift -= FinishCutscene;
        }

        public void FinishCutscene()
        {
            StartCoroutine(FinishScene());
        }


        public IEnumerator Jump()
        {
            _canJump = false;
            yield return new WaitForSeconds(0.2f);
            _canJump = true;
        }

        private bool _canJump = true;
        private Camera _camera;
    }
}