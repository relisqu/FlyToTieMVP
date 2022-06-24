using System;
using DG.Tweening;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    public Unit unit;
    public float duration;
    
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Unit>(out Unit _otherUnit))
        {
            Debug.Log("Unit entered attraction zone");    
            if (_otherUnit.unitState == UnitState.Attached)
            {
                Debug.Log("Movement started");
                Tween movement = unit.transform
                    .DOMove(Unit.GetAttachmentPosition(unit), duration)
                    .SetEase(Ease.InOutExpo)
                    .OnComplete(unit.AttachTo);
                gameObject.SetActive(false);
            }
        }
    }
}