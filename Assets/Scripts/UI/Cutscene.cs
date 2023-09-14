using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Generation;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DefaultNamespace.UI
{
    public class Cutscene : MonoBehaviour
    {
        public static bool IsPlayingCutscene;
        public static bool LockedPlayerInputMovement;
        [SerializeField] public List<CanvasGroup> DisabledUIScenes;
        [SerializeField] public List<CanvasGroup> EnabledUIScenes;
        [SerializeField] protected PlayerMovement PlayerMovement;

        private void Start()
        {
            if (PlayerMovement == null)
            {
                PlayerMovement = FindObjectOfType<PlayerMovement>();
            }
        }

        public void PlayCutscene()
        {
            gameObject.SetActive(true);
            StartCoroutine(EnableScenes(DisabledUIScenes));
            StartCoroutine(DisableScenes(EnabledUIScenes));
            AdditionalCutsceneStart();
            
            if (IsPlayingCutscene) return;
            IsPlayingCutscene = true;
            
        }

        public virtual void AdditionalCutsceneStart()
        {
            
            PlayerMovement.DisableMovement();
            PlayerFollow.Instance.ResetPosition();
        }
        public virtual void AdditionalCutsceneStop()
        {
            PlayerMovement.EnableMovement();
            PlayerMovement.Jump();
        }
        public void StopCutscene()
        {
            AdditionalCutsceneStop();
            IsPlayingCutscene = false;
            StartCoroutine(EnableScenes(EnabledUIScenes));
            StartCoroutine(DisableScenes(DisabledUIScenes));
            gameObject.SetActive(false);
        }

        public IEnumerator DisableScenes( List<CanvasGroup> scenes)
        {
            var delay = 0.06f;
            foreach (var scene in scenes)
            {
                scene.DOFade(0f, delay).OnComplete(() => { scene.gameObject.SetActive(false); });
                print("FF");
            }
            yield return new WaitForSeconds(delay);

        }

        public IEnumerator EnableScenes(List<CanvasGroup> scenes)
        {
            var delay = 0.2f;
            
            foreach (var scene in scenes)
            {
                scene.gameObject.SetActive(true);
                scene.DOFade(1f, delay).OnComplete(() => {  });
            }
            yield return new WaitForSeconds(delay);
            
        }

      

    
    }
}