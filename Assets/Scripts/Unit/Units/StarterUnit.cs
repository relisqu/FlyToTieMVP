using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class StarterUnit : Unit
{

    [SerializeField] private ObstacleDamageable Damageable;

    protected override void OnObstacleCollision(Obstacle obstacle)
    {
        if (BottomUnit == this)
        {
            Debug.Log("Lost");
            return;
        }

        base.OnObstacleCollision(obstacle);
    }


    public override void OnJump()
    {
    }

    public static void SetInvisible(bool value)
    {
        _isInvisible = value;
    }

    public static bool IsInvisible()
    {
        return _isInvisible;
    }

    private static bool _isInvisible;

    private void Start()
    {
        SetState(UnitState.Attached);
        OnDamageTaken += Damageable.TakeDamage;
        BottomUnit = this;
    }
}