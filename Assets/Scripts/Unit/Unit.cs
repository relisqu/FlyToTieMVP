using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public static Unit unitBottom;

    public Vector3 offsetOnAttachment;
    public UnitState unitState;
    
    [HideInInspector]
    public Unit unitAbove;
    [HideInInspector]
    public Unit unitBelow;

    public void AttachTo(Unit unit)
    {
        if (unitState != UnitState.Unattached)
        {
            return;
        }
        
        transform.SetParent(unitBottom.transform, true);
        transform.position = unitBottom.transform.position + offsetOnAttachment;

        unitAbove = unitBottom;
        unitAbove.unitBelow = this;
        unitBottom = this;

        unitState = UnitState.Attached;
    }
    
    public void DamageSelf()
    {
        if (unitBelow != null)
        {
            unitBelow.DamageSelf();
        }
        transform.SetParent(null, true);
        unitState = UnitState.Dropped;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        Obstacle _obstacle;
        if (col.gameObject.TryGetComponent<Obstacle>(out _obstacle))
        {
            DamageSelf();
        }

        Unit _otherUnit;
        if (col.gameObject.TryGetComponent<Unit>(out _otherUnit))
        {
            _otherUnit.AttachTo(this);
        }
    }

    public abstract void OnJump();
}
