using UnityEngine;

public class ShootingUnit : Unit
{
    [SerializeField] protected Shooter Shooter;
    [SerializeField] protected bool IsShootingViaAnimation;

    protected override void AttachTo()
    {
        base.AttachTo();
        Shooter.enabled = true;
    }

    public override void OnJump()
    {
        if (Shooter.enabled && !IsShootingViaAnimation) Shooter.Shoot();
    }

    public void Shoot()
    {
        if (Shooter.enabled) Shooter.Shoot();
    }
    
    public void StopShooting()
    {
        if (Shooter.enabled) Shooter.StopShooting();
    }
}