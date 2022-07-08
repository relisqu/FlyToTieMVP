using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using Projectiles;
using UnityEngine;

public class LaserProjectile : Projectile
{
    [SerializeField] private LineRenderer LaserBody;
    [SerializeField] private float MaxRange;
    [SerializeField] private float LaserLife;
    [SerializeField] private LayerMask LaserIgnoreLayers;
    [SerializeField] private Transform ColliderTransform;
    [SerializeField] private List<ParticleSystem> Particles;

    public void EmitAllParticles()
    {
        foreach (var particle in Particles)
        {
            
            particle.Play();
        }
    }
    public void StopAllParticles()
    {
        foreach (var particle in Particles)
        {
            
            particle.Stop();
        }
    }
    public override void SpawnProjectile()
    {
        if (_isEnabled) return;
        ColliderTransform.gameObject.SetActive(true);
        LaserBody.enabled = true;
        _isEnabled = true;
        StartCoroutine(UpdateLaserLife());
        EmitAllParticles();
    }

    public override void DestroyProjectile()
    {
        LaserBody.positionCount = 0;
        _isEnabled = false;
        StopAllCoroutines();
        StopAllParticles();
        ColliderTransform.gameObject.SetActive(false);
    }

    IEnumerator UpdateLaserLife()
    {
        LaserBody.widthMultiplier = 0.01f;
        var laserTween = DOTween.To(() => LaserBody.widthMultiplier, x => LaserBody.widthMultiplier = x, 1,
            LaserLife / 5);
        yield return new WaitForSeconds(LaserLife * 3 / 5);
        laserTween.PlayBackwards();

        yield return new WaitForSeconds(LaserLife / 5);
        DestroyProjectile();
    }

    void DrawLaser()
    {
        var firstPoint = transform.position;
        Vector3 secondPoint;

        var raycast = Physics2D.Raycast(firstPoint, Vector2.right, MaxRange, LaserIgnoreLayers);
        if (raycast)
        {
            secondPoint = new Vector2(raycast.point.x, firstPoint.y);
        }
        else
        {
            secondPoint = firstPoint + Vector3.right * MaxRange;
        }

        LaserBody.positionCount = 2;
        LaserBody.SetPosition(0, firstPoint);
        LaserBody.SetPosition(1, secondPoint);
        ColliderTransform.position = secondPoint;
    }

    private void Update()
    {
        if (_isEnabled) DrawLaser();
    }

    private bool _isEnabled;
}