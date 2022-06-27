using UnityEngine;

public class ShootingUnit : Unit
{
    [SerializeField]private Shooter Shooter;

    protected override void AttachTo()
    {
        base.AttachTo();
        Shooter.enabled = true;
    }

    public override void OnJump()
    {
        if (Shooter.enabled) Shooter.OnShoot();
    }
}