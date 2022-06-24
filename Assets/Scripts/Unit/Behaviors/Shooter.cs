using System;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    public List<Projectile> projectiles;

    private Queue<Projectile> queuedProjectiles;

    private float lastShotTime = 0;

    private void Start()
    {
        queuedProjectiles = new Queue<Projectile>();
    }

    public void OnShoot()
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
        
        Debug.Log("Trying to shoot");
        
        Projectile curProj = queuedProjectiles.Peek();
        
        if (Time.time - lastShotTime > curProj.delay)
        {
            Instantiate(curProj.gameObject, transform.position + curProj.spawnPosOffset, Quaternion.identity);
            queuedProjectiles.Dequeue();
            lastShotTime = Time.time;
        }
    }
}