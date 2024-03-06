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

    private float _laserWidth = 1f;

   

    public void SetWidth(float width)
    {
        _laserWidth = width;
    }

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
        LaserAnimator.SetTrigger("StartLaser");
       // var laserTween = DOTween.To(() => LaserBody.widthMultiplier, x => LaserBody.widthMultiplier = x, Scale,
        //    LaserLife / 3).SetAutoKill(false);

       // transform.localScale = new Vector3(1, 0.1f, 1);
       // var defaultTween = DOTween.To(() => transform.localScale.y,
       //     x => transform.localScale = Vector3.one * (Mathf.Clamp(Scale * 0.6f, 1f, 3f)),
       //     Scale,
       //     LaserLife / 5).SetAutoKill(false);
        //ColliderTransform.localScale = Vector3.one * (0.4f * Scale);
        AudioManager.instance.Play("laser_shot");
        Debug.Log("Started laser");
        yield return new WaitForSeconds(LaserLife * 3 / 5);
        //laserTween.PlayBackwards();
        //defaultTween.PlayBackwards();
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
        LaserBody.widthMultiplier = Scale;
        Debug.Log("scale "+Scale+$" {Time.time}");
        if (_isEnabled)
        {
            DrawLaser();
        }
        else
        {
            SetParticlesAtSecondPoint();
        }
    }

    private void OnEnable()
    {
        DestroyProjectile();
    }

    private bool _isEnabled;
}