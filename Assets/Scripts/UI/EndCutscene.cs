using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class EndCutscene : Cutscene
    {
        public static bool IsPlayingEndCutscene;
        [SerializeField] private float TransformYPosition;

        [SerializeField] private StartCutscene StartCutscene;

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
            yield return StartCoroutine(StartCutscene.DisableScenes(StartCutscene.DisabledUIScenes));
            yield return DisableScenes(DisabledUIScenes);
            print("FFFFFFFFFFFFFFFF");
            PlayerFollow.Instance.StopFollowing();
            yield return new WaitForSeconds(4f);
            StarterUnit.Instance.RemoveAllChildren();
            IsPlayingEndCutscene = false;
            LockedPlayerInputMovement = false;
            StartCutscene.PlayCutscene();
            gameObject.SetActive(false);
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