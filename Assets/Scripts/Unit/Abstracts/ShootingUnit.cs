using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingUnit : Unit
{
    public List<Projectile> projectiles;

    private Queue<Projectile> queuedProjectiles;

    private float lastShotTime = 0;
    
    public override void OnJump()
    {
        foreach (var projectile in projectiles)
        {
            queuedProjectiles.Enqueue(projectile);
        }
    }

    protected virtual void Update()
    {
        if (queuedProjectiles.Count == 0)
        {
            return;
        }
        
        Projectile curProj = queuedProjectiles.Peek();
        
        if (Time.time - lastShotTime > curProj.delay)
        {
            Instantiate(curProj.gameObject, transform.position + curProj.spawnPosOffset, Quaternion.identity);
            queuedProjectiles.Dequeue();
        }
    }
}