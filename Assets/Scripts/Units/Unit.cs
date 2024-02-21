using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.UI;
using DG.Tweening;
using Units;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour
{
    protected static Unit BottomUnit;

    public static Action OnDamageTaken;
    public Action OnCurrentUnitDamageTaken;

    [FormerlySerializedAs("collider")] [SerializeField]
    private Collider2D Collider;

    [FormerlySerializedAs("offsetOnAttachment")] [SerializeField]
    private Vector3 OffsetOnAttachment;

    [SerializeField] private Vector3 OffsetOnChildAttachment;

    [SerializeField] public UnitAnimator Animator;

    private Unit _aboveUnit;
    private Unit _belowUnit;

    public UnitState UnitState { get; protected set; }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        print("Collided: " + name);
        if (col.gameObject.TryGetComponent(out Obstacle.Obstacle obstacle)) OnObstacleCollision(obstacle);

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

    public virtual void AnimateJump()
    {
        if (Animator != null) Animator.Jump();
    }


    protected virtual void AttachTo()
    {
        if (UnitState != UnitState.Unattached) return;

        transform.SetParent(BottomUnit.transform, true);
        transform.position = BottomUnit.transform.position + OffsetOnAttachment + BottomUnit.OffsetOnChildAttachment;
        PlayerMovement.Jumped += OnJump;
        PlayerMovement.Jumped += AnimateJump;
        DOTween.Kill(transform);
        _aboveUnit = BottomUnit;
        AudioManager.instance.Play("new_unit");
        _aboveUnit.SetBelowUnit(this);
        BottomUnit = this;
        BottomUnit.UnitState = UnitState.Attached;
        BottomUnit.Animator.SetTag("Idle");
    }

    protected virtual void OnEnable()
    {
        UnitState = UnitState.Unattached;
    }

    protected virtual void OnObstacleCollision(Obstacle.Obstacle obstacle)
    {
        if (UnitState == UnitState.Attached)
            DamageSelf();
    }

    public void TakeDamage()
    {
        OnCurrentUnitDamageTaken?.Invoke();
        Animator.TakeDamage();
    }

    public void DamageSelf()
    {
        if (StarterUnit.IsInvincible() || Cutscene.IsPlayingCutscene) return;
        print("Took damage: " + name);
        print("Damaged: " + BottomUnit.name);
        OnDamageTaken?.Invoke();
        DestroyBottomUnit();
    }

    public void DestroyBottomUnit()
    {
        PlayerMovement.Jumped -= BottomUnit.OnJump;
        PlayerMovement.Jumped -= BottomUnit.AnimateJump;
        AudioManager.instance.Play("unit_lost");
        BottomUnit.Collider.enabled = false;
        BottomUnit.transform.SetParent(null, true);
        BottomUnit.UnitState = UnitState.Dropped;
        BottomUnit.TakeDamage();
        BottomUnit.transform.parent = null;
        BottomUnit = BottomUnit._aboveUnit;
        BottomUnit.SetBelowUnit(null);
    }

    private void OnDisable()
    {
        PlayerMovement.Jumped -= OnJump;
        PlayerMovement.Jumped -= AnimateJump;
    }

    protected void SetBelowUnit(Unit unit)
    {
        _belowUnit = unit;
    }

    void OnBecameInvisible()
    {
        if (UnitState == UnitState.Dropped)
            Destroy(gameObject);
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