using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI
{
    public class StartCutscene : Cutscene, IPointerClickHandler
    {
        private void OnEnable()
        {
            PlayCutscene();
            PlayerFollow.Instance.Follow();
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