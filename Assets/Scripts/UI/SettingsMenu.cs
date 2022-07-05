using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI
{
    public class SettingsMenu : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Transform MenuTransform;
        [SerializeField] private CanvasGroup MenuAlpha;
        [SerializeField] private float FinalScale;
        private bool _isFullVisible;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isFullVisible)
            {
                MenuTransform.DOScaleX(0, 0.1f);
                MenuAlpha.DOFade(0, 0.1f);
            }
            else
            {
                MenuTransform.DOScaleX(FinalScale, 0.1f);
                MenuAlpha.DOFade(1, 0.1f);
            }

            _isFullVisible = !_isFullVisible;
        }

        private void Start()
        {
            
            MenuTransform.DOScaleX(0, 0.1f);
            MenuAlpha.DOFade(0, 0.1f);
        }
    }
}