using Projectiles;
using UnityEngine;

public class MovingProjectile : Projectile
{
    public HorizontalMovement MovementController;


    public override void SpawnProjectile()
    {
        MovementController.SetSpeed(Speed);
    }

    public override void DestroyProjectile()
    {
    }
}