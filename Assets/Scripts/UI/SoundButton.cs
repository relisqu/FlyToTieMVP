using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class SoundButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image Alpha;

        private bool _isEnabled = true;
        public string[] Sounds;
        [SerializeField] private string Tag;

        private void Start()
        {
            _isEnabled = PlayerPrefs.GetInt(Tag, 0) == 1;
            Alpha.DOFade(_isEnabled ? 1f : 0.5f, 0.2f);
            foreach (var sound in Sounds)
            {
                if (_isEnabled)
                {
                    AudioManager.instance.ResetVolume(sound);
                }
                else
                {
                    AudioManager.instance.ChangeVolume(sound, 0f);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Alpha.DOFade(_isEnabled ? 0.5f : 1f, 0.2f);

            _isEnabled = !_isEnabled;
            PlayerPrefs.SetInt(Tag, _isEnabled ? 1 : 0);
            foreach (var sound in Sounds)
            {
                if (_isEnabled)
                {
                    AudioManager.instance.ResetVolume(sound);
                }
                else
                {
                    AudioManager.instance.ChangeVolume(sound, 0f);
                }
            }
        }
    }
}