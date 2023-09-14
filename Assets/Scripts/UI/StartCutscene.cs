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
            LevelGenerator.Instance.SpawnLevel();
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
                    StopCutscene();
                    break;
            }
            OnGameplayStart?.Invoke();
        }
        
    }
}