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
            EnableScenes(DisabledUIScenes);
            DisableScenes(EnabledUIScenes);
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
            EnableScenes(EnabledUIScenes);
            DisableScenes(DisabledUIScenes);
            gameObject.SetActive(false);
        }

        public void DisableScenes( List<CanvasGroup> scenes)
        {
            var delay = 0.06f;
            foreach (var scene in scenes)
            {
                scene.DOFade(0f, delay).OnComplete(() => { scene.gameObject.SetActive(false); });
            }

        }

        public void EnableScenes(List<CanvasGroup> scenes)
        {
            
            foreach (var scene in scenes)
            {
                scene.gameObject.SetActive(true);
                scene.DOFade(1f, 0.2f).OnComplete(() => {  });
            }
            
        }

      

    
    }
}