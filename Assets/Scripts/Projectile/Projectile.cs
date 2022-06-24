using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public HorizontalSpeed speedController;
    public float initialSpeed;
    public float delay;
    public Vector3 spawnPosOffset;

    private void Start()
    {
        speedController.maintainedSpeed = initialSpeed;
    }
}