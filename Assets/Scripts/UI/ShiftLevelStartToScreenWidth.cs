using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class ShiftLevelStartToScreenWidth : MonoBehaviour
    {
        private bool _neededToShift = true;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
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
        }

        private void StartShifting()
        {
            _neededToShift = true;
        }


        private void ShiftLevelStart()
        {
            if (!_neededToShift) return;
            var x = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.nearClipPlane));

            transform.position = new Vector3(x.x, transform.position.y, 0);
        }
    }
}