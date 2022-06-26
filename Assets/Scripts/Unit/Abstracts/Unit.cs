using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour
{
    protected static Unit BottomUnit;

    [FormerlySerializedAs("collider")] [SerializeField]
    private Collider2D Collider;

    [FormerlySerializedAs("offsetOnAttachment")] [SerializeField]
    private Vector3 OffsetOnAttachment;

    public UnitState UnitState { get; private set; }


    private Unit _aboveUnit;
    private Unit _belowUnit;

    public static Action OnDamageTaken;
    public abstract void OnJump();

    public void SetState(UnitState state)
    {
        UnitState = state;
    }

    public Unit GetAboveUnit()
    {
        return _aboveUnit;
    }

    public void SetAboveUnit(Unit unit)
    {
        _aboveUnit = unit;
    }

    public Unit GetBelowUnit()
    {
        return _belowUnit;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            OnObstacleCollision(obstacle);
        }

        if (col.gameObject.TryGetComponent(out Unit unit))
        {
            OnUnitCollision(unit);
        }
    }

    public static Vector3 GetAttachmentPosition(Unit unit)
    {
        return BottomUnit.transform.position + unit.OffsetOnAttachment;
    }

    protected virtual void AttachTo()
    {
        if (UnitState != UnitState.Unattached)
        {
            return;
        }

        transform.SetParent(BottomUnit.transform, true);
        transform.position = BottomUnit.transform.position + OffsetOnAttachment;

        _aboveUnit = BottomUnit;
        _aboveUnit.SetBelowUnit(this);
        BottomUnit = this;

        UnitState = UnitState.Attached;
    }

    protected virtual void OnObstacleCollision(Obstacle obstacle)
    {
        if (UnitState == UnitState.Attached)
            DamageSelf();
    }

    private void DamageSelf()
    {
        if (StarterUnit.IsInvisible()) return;
        OnDamageTaken?.Invoke();
        BottomUnit.Collider.enabled = false;
        BottomUnit.transform.SetParent(null, true);
        BottomUnit = BottomUnit._aboveUnit;
        UnitState = UnitState.Dropped;
    }

    private void SetBelowUnit(Unit unit)
    {
        _belowUnit = unit;
    }


    private void OnUnitCollision(Unit unit)
    {
        if (UnitState == UnitState.Attached && unit.UnitState == UnitState.Unattached)
            unit.AttachTo();
    }


    public Vector3 GetAttachOffset()
    {
        return OffsetOnAttachment;
    }
}