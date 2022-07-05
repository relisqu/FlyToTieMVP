using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Units;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour
{
    protected static Unit BottomUnit;

    public static Action OnDamageTaken;

    [FormerlySerializedAs("collider")] [SerializeField]
    private Collider2D Collider;

    [FormerlySerializedAs("offsetOnAttachment")] [SerializeField]
    private Vector3 OffsetOnAttachment;

    [SerializeField] protected UnitAnimator Animator;

    private Unit _aboveUnit;
    private Unit _belowUnit;

    public UnitState UnitState { get; private set; }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        print("Collided: " + name);
        if (col.gameObject.TryGetComponent(out Obstacle obstacle)) OnObstacleCollision(obstacle);

        if (col.gameObject.TryGetComponent(out Unit unit)) OnUnitCollision(unit);
    }

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

    public static Vector3 GetAttachmentPosition(Unit unit)
    {
        return BottomUnit.transform.position + unit.OffsetOnAttachment;
    }

    void AnimateJump()
    {
        if (Animator != null) Animator.Jump();
    }


    protected virtual void AttachTo()
    {
        if (UnitState != UnitState.Unattached) return;

        transform.SetParent(BottomUnit.transform, true);
        transform.position = BottomUnit.transform.position + OffsetOnAttachment;
        DOTween.Kill(transform);
        _aboveUnit = BottomUnit;
        _aboveUnit.SetBelowUnit(this);
        BottomUnit = this;
        PlayerMovement.Jumped += OnJump;
        PlayerMovement.Jumped += AnimateJump;
        BottomUnit.UnitState = UnitState.Attached;
    }

    protected virtual void OnObstacleCollision(Obstacle obstacle)
    {
        if (UnitState == UnitState.Attached)
            DamageSelf();
    }

    public void TakeDamage()
    {
        Animator.TakeDamage();
    }

    private void DamageSelf()
    {
        if (StarterUnit.IsInvincible()) return;
        print("Took damage: " + name);
        print("Damaged: " + BottomUnit.name);
        OnDamageTaken?.Invoke();
        BottomUnit.Collider.enabled = false;
        BottomUnit.transform.SetParent(null, true);
        BottomUnit.UnitState = UnitState.Dropped;
        BottomUnit.TakeDamage();
        BottomUnit = BottomUnit._aboveUnit;
        PlayerMovement.Jumped -= OnJump;
        PlayerMovement.Jumped -= AnimateJump;


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