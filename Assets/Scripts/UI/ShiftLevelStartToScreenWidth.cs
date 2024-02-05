using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class ShiftLevelStartToScreenWidth : MonoBehaviour
    {
        private bool _neededToShift = true;

        private void Start()
        {
            StartCutscene.OnGameplayStart += StopShifting;
            EndCutscene.OnGameplayFinish += StartShifting;
        }

        private void OnDestroy()
        {
            StartCutscene.OnGameplayStart -= StopShifting;
            EndCutscene.OnGameplayFinish -= StartShifting;
        }

        private void Update()
        {
            ShiftLevelStart();
        }

        private void StopShifting()
        {
            _neededToShift = false;
            Debug.Log("stop shift");
        }

        private void StartShifting()
        {
            _neededToShift = true;
            Debug.Log("start shift");
        }


        private void ShiftLevelStart()
        {
            if (!_neededToShift) return;
            Debug.Log("Shifting");
            var x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

            transform.position = new Vector3(x.x,  transform.position.y, 0);
        }
    }
}