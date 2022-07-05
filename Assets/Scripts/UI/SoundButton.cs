using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class SoundButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image Alpha;

        private bool _isEnabled=true;

        public void OnPointerDown(PointerEventData eventData)
        {
            Alpha.DOFade(_isEnabled ? 0.5f : 1f, 0.2f);

            _isEnabled = !_isEnabled;
        }
        
    }
}