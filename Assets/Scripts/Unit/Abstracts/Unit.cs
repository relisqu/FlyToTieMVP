using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected static Unit BottomUnit;

    public Collider2D collider;
    
    public Vector3 offsetOnAttachment;
    public UnitState unitState;
    
    [SerializeField] private Unit aboveUnit;
    [SerializeField] private Unit belowUnit;

    public Unit GetAboveUnit()
    {
        return aboveUnit;
    }

    public void SetAboveUnit(Unit unit)
    {
        aboveUnit = unit;
    }

    public Unit GetBelowUnit()
    {
        return belowUnit;
    }

    public void SetBelowUnit(Unit unit)
    {
        belowUnit = unit;
    }

    public void AttachTo(Unit unit)
    {
        if (unitState != UnitState.Unattached)
        {
            return;
        }
        
        transform.SetParent(BottomUnit.transform, true);
        transform.position = BottomUnit.transform.position + offsetOnAttachment;

        aboveUnit = BottomUnit;
        aboveUnit.SetBelowUnit(this);
        BottomUnit = this;

        unitState = UnitState.Attached;
    }
    
    public void DamageSelf()
    {
        BottomUnit.collider.enabled = false;
        BottomUnit.transform.SetParent(null, true);
        BottomUnit = BottomUnit.aboveUnit;
        unitState = UnitState.Dropped;
    }

    protected virtual void OnObstacleCollision(Obstacle obstacle)
    {
        DamageSelf();
    }

    protected virtual void OnUnitCollision(Unit unit)
    {
        if (unitState == UnitState.Attached && unit.unitState == UnitState.Unattached)
            unit.AttachTo(this);
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Obstacle>(out Obstacle _obstacle))
        {
            OnObstacleCollision(_obstacle);
        }

        if (col.gameObject.TryGetComponent<Unit>(out Unit _unit))
        {
            OnUnitCollision(_unit);
        }
    }

    public abstract void OnJump();
}
