using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HorizontalMovement : MonoBehaviour
{
    [FormerlySerializedAs("maintainedSpeed")] [SerializeField]
    private float Speed;

    public Rigidbody2D rigidbody;

    public void Move(float newSpeed)
    {
        float requiredForce = newSpeed - rigidbody.velocity.x;
        rigidbody.AddForce(Vector2.right * requiredForce, ForceMode2D.Force);
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    private void Update()
    {
        Move(Speed);
    }
}