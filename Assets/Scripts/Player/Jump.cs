using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    public Vector2 jumpAcceleration;
    public float cooldown = 0.3f;

    private float _lastJumpTime = -100;
    
    public void OnJump()
    {
        if (Time.time - _lastJumpTime > cooldown)
        {
            _lastJumpTime = Time.time;
        }
        else
        {
            return;
        }
        rigidbody.AddForce(jumpAcceleration, ForceMode2D.Impulse);
    }
}