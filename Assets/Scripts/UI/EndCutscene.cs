using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class EndCutscene : Cutscene
    {
        public static bool IsPlayingEndCutscene;
        [SerializeField] private float TransformYPosition;

        [SerializeField] private StartCutscene StartCutscene;
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
            if (_canJump && IsPlayingEndCutscene && PlayerMovement.transform.position.y < TransformYPosition)
            {
                StartCoroutine(Jump());
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
            yield return DisableScenes(DisabledUIScenes);

            if ((PlayerData.СurrentLevel-1) % 4 == 0)
            {
                
                yield return StartCoroutine(EnableScenes(GiftScenes));
            }
            else
            {
                yield return StartCoroutine(FinishScene());
            }
        }

        IEnumerator FinishScene()
        {
            yield return DisableScenes(GiftScenes);
            PlayerFollow.Instance.StopFollowing();
            yield return new WaitForSeconds(4f);
            StarterUnit.Instance.RemoveAllChildren();
            IsPlayingEndCutscene = false;
            LockedPlayerInputMovement = false;
            FinishedScene?.Invoke();
            gameObject.SetActive(false);
        }

        public static  Action FinishedScene;
        private void Start()
        {
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
    }
}