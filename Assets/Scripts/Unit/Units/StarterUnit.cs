using System;
using UnityEngine;

public class StarterUnit : Unit
{
    public Jump jump;

    protected override void OnObstacleCollision(Obstacle obstacle)
    {
        if (BottomUnit == this)
        {
            Debug.Log("Lost");
            return;
            // TO-DO losing case
        }

        base.OnObstacleCollision(obstacle);
    }


    public override void OnJump()
    {
        jump.OnJump();
    }

    private void Start()
    {
        unitState = UnitState.Attached;
        BottomUnit = this;
    }
}