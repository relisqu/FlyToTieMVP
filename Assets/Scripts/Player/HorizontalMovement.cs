﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HorizontalMovement : MonoBehaviour
{
    [FormerlySerializedAs("maintainedSpeed")] [SerializeField]
    private float Speed;

    public Rigidbody2D rigidbody;
    private bool _isMoving = true;

    private void Start()
    {
        _defaultSpeed = Speed;
    }

    public void ResetSpeed()
    {
        Speed = _defaultSpeed;
    }

    private void Update()
    {
        if (_isMoving)
            Move(Speed);
    }

    private float _defaultSpeed;
    public void Move(float newSpeed)
    {
        var requiredForce = newSpeed - rigidbody.velocity.x;
        rigidbody.AddForce(Vector2.right * requiredForce, ForceMode2D.Force);
    }

    public void SetMovementState(bool value)
    {
        _isMoving = value;
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }
}