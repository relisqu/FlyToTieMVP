using System;
using UnityEngine;

public class ShootingUnit : Unit
{
    [SerializeField] public Shooter Shooter;
    [SerializeField] protected bool IsShootingViaAnimation;

    private void Start()
    {
        OnCurrentUnitDamageTaken += Shooter.StopShooting;
    }


    private void OnDestroy()
    {
        OnCurrentUnitDamageTaken -= Shooter.StopShooting;
    }

    protected override void AttachTo()
    {
        base.AttachTo();
        Shooter.enabled = true;
    }

    public override void OnJump()
    {
        if (Shooter != null && Shooter.enabled && !IsShootingViaAnimation) Shooter.Shoot();
    }

    public void Shoot()
    {
        if (Shooter != null && Shooter.enabled) Shooter.Shoot();
    }

    public void StopShooting()
    {
        if (Shooter.enabled) Shooter.StopShooting();
    }
}