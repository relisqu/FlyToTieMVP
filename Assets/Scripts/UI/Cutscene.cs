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
    public class Cutscene : MonoBehaviour, IPointerClickHandler
    {
        public static bool IsPlayingCutscene;
        [SerializeField] private List<CanvasGroup> DisabledUIScenes;
        [SerializeField] private List<CanvasGroup> EnabledUIScenes;
        [SerializeField] private PlayerMovement PlayerMovement;

        public void PlayCutscene()
        {
            gameObject.SetActive(true);
            if (IsPlayingCutscene) return;
            IsPlayingCutscene = true;
            PlayerMovement.DisableMovement();
            
        }

        public void StopCutscene()
        {
            
            PlayerMovement.EnableMovement();
            PlayerMovement.Jump();
            FullGenerator.Instance.GenerateLevel();
            IsPlayingCutscene = false;
            StartCoroutine(DisableScenes());
        }

        public IEnumerator DisableScenes()
        {
            yield return EnableScenes();
            var delay = 0.06f;
            foreach (var scene in DisabledUIScenes)
            {
                scene.DOFade(0f, delay).OnComplete(() => { scene.gameObject.SetActive(false); });
            }
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);

        }

        public IEnumerator EnableScenes()
        {
            var delay = 0.2f;
            
            foreach (var scene in EnabledUIScenes)
            {
                scene.gameObject.SetActive(true);
                scene.DOFade(1f, delay).OnComplete(() => {  });
            }
            yield return new WaitForSeconds(delay/2);
            
        }

        private void Start()
        {
            PlayCutscene();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (IsPlayingCutscene)
            {
                case false:
                    return;
                case true:
                    StopCutscene();
                    break;
            }
        }
    }
}