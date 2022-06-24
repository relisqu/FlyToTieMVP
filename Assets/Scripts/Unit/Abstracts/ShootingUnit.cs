using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUnit : Unit
{
    public Shooter shooter;

    public override void AttachTo()
    {
        base.AttachTo();
        shooter.enabled = true;
    }

    public override void OnJump()
    {
        if (shooter.enabled)
        {
            shooter.OnShoot();
        }
    }
}