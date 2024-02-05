using System;
using DefaultNamespace.Generation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI
{
    public class StartCutscene : Cutscene, IPointerClickHandler
    {
        public static Action OnGameplayStart;
        private void OnEnable()
        {
            PlayCutscene();
            
            StarterUnit.Instance.SetAlive();
            IsPlayingCutscene = true;
            PlayerFollow.Instance.Follow();
        }

        private void Awake()
        {
            EndCutscene.FinishedScene+=PlayCutscene;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (IsPlayingCutscene)
            {
                case false:
                    return;
                case true:
                    Debug.Log("Starting");
                    StopCutscene();
                    break;
            }
            OnGameplayStart?.Invoke();
        }
        
    }
}