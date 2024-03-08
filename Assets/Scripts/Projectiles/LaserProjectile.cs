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
    [SerializeField] private float LaserLife;
    [SerializeField] private Animator LaserAnimator;
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

    public override void SetScale(float scale)
    {
        Scale = scale;
    }

    Coroutine laserCoroutine;

    public override void SpawnProjectile()
    {
        if (_isEnabled) return;
        gameObject.SetActive(true);
        EmitAllParticles();
        LaserBody.enabled = true;
        laserCoroutine = StartCoroutine(UpdateLaserLife());
    }

    public override void DestroyProjectile()
    {
        if (!_isEnabled) return;
        LaserBody.positionCount = 0;
        LaserBody.enabled = false;
        gameObject.SetActive(false);
        _isEnabled = false;
    }

    IEnumerator UpdateLaserLife()
    {
        _isEnabled = true;
        LaserAnimator.SetTrigger("StartLaser");
        var laserTween = DOTween.To(() => 0.01f, x => LaserBody.widthMultiplier = x, Scale,
            LaserLife / 3).SetAutoKill(false);

        var defaultTween = ColliderTransform.DOScale(Math.Clamp(Scale * 0.8f, 1f, 3f), LaserLife / 5);
        ColliderTransform.localScale = Vector3.one * (0.8f * Scale);
        AudioManager.instance.Play("laser_shot");
        Debug.Log("Started laser");
        yield return new WaitForSeconds(LaserLife * 3 / 5);
        //laserTween.PlayBackwards();
        //defaultTween.PlayBackwards();
        StopAllParticles();
        LaserAnimator.SetTrigger("FinishLaser");

        yield return new WaitForSeconds(LaserLife / 5);
        DestroyProjectile();
    }


    Vector3 SetParticlesAtSecondPoint()
    {
        var firstPoint = transform.position;
        Vector3 secondPoint;
        var maxPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        var range = maxPoint - firstPoint.x;
        var raycast = Physics2D.Raycast(firstPoint, Vector2.right, range, LaserIgnoreLayers);
        if (raycast)
        {
            secondPoint = new Vector2(raycast.point.x, firstPoint.y);
        }
        else
        {
            secondPoint = firstPoint + range * Vector3.right;
        }

        ColliderTransform.position = secondPoint;
        return secondPoint;
    }

    void DrawLaser()
    {
        var firstPoint = transform.position;


        LaserBody.positionCount = 2;
        LaserBody.SetPosition(0, firstPoint);
        LaserBody.SetPosition(1, SetParticlesAtSecondPoint());
    }

    private void OnDrawGizmos()
    {
        var firstPoint = transform.position;
        var maxPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        var range = maxPoint - firstPoint.x;
        var raycast = Physics2D.Raycast(firstPoint, Vector2.right, range, LaserIgnoreLayers);
        if (raycast)
        {
            Gizmos.DrawSphere(new Vector3(raycast.point.x, firstPoint.y), 1f);
        }

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(ColliderTransform.position, 1f);
    }

    private void Update()
    {
        DrawLaser();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private bool _isEnabled;
}