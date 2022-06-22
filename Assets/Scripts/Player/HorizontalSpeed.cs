using System;
using UnityEngine;

public class HorizontalSpeed : MonoBehaviour
{
    public float initialSpeed;
    public Rigidbody2D rigidbody;
    
    public void SetSpeed(float newSpeed)
    {
        float requiredForce = newSpeed - rigidbody.velocity.x;
        rigidbody.AddForce(Vector2.right * requiredForce, ForceMode2D.Impulse);
    }

    private void Start()
    {
        SetSpeed(initialSpeed);
    }
}
