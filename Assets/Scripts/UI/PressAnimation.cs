using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI
{
    public class PressAnimation : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]private float PressDuration=0.35f;
        [SerializeField]private Vector3 MaxScale=new Vector3(1.15f, 1f, 1f);
        [SerializeField]private Vector3 PositionShift=new Vector3(0f, 1f, 1f);
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isPressing) return;
            _isPressing = true;
            transform.DOPunchScale(MaxScale, PressDuration).OnComplete(() => { _isPressing = false; });
            transform.DOPunchPosition(PositionShift, PressDuration);
        }

        private bool _isPressing;
    }
}