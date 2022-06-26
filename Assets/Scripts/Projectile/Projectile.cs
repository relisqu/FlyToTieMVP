using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [FormerlySerializedAs("speedController")] public HorizontalMovement MovementController;
    public float initialSpeed;
    public float delay;
    public Vector3 spawnPosOffset;

    private void Start()
    {
        MovementController.SetSpeed(initialSpeed);
    }
}