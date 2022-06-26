using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ShootingUnit : Unit
{
    public Shooter shooter;

    protected override void AttachTo()
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