using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public List<Projectile> projectiles;

    private float lastShotTime;

    private Queue<Projectile> queuedProjectiles;

    private void Start()
    {
        queuedProjectiles = new Queue<Projectile>();
    }

    protected virtual void Update()
    {
        if (queuedProjectiles.Count == 0) return;

        Debug.Log("Trying to shoot");

        var curProj = queuedProjectiles.Peek();

        if (Time.time - lastShotTime > curProj.delay)
        {
            Instantiate(curProj.gameObject, transform.position + curProj.spawnPosOffset, Quaternion.identity);
            queuedProjectiles.Dequeue();
            lastShotTime = Time.time;
        }
    }

    public void OnShoot()
    {
        foreach (var projectile in projectiles) queuedProjectiles.Enqueue(projectile);
    }
}