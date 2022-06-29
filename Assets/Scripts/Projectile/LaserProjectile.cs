using System;
using System.Collections;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class LaserProjectile : Projectile
{
    [SerializeField] private LineRenderer LaserBody;
    [SerializeField] private float MaxRange;
    [SerializeField] private float LaserLife;
    [SerializeField] private LayerMask LaserIgnoreLayers;
    [SerializeField] private Transform ColliderTransform;

    public override void SpawnProjectile()
    {
        if (_isEnabled) return;
        print("Started laser");
        LaserBody.enabled = true;
        _isEnabled = true;
        StartCoroutine(UpdateLaserLife());
    }

    protected override void DestroyProjectile()
    {
        LaserBody.positionCount = 0;
        _isEnabled = false;
        print("Stopped laser");
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

        var raycast = Physics2D.Raycast(firstPoint, Vector2.right, float.PositiveInfinity, LaserIgnoreLayers);
        if (raycast)
        {
            secondPoint = new Vector2(raycast.transform.position.x, firstPoint.y);
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