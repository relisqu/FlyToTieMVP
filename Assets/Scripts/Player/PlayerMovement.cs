using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementState
    {
        Move,
        TakeDamage
    }

    [FormerlySerializedAs("rigidbody")] [SerializeField]
    private Rigidbody2D Rigidbody;

    [SerializeField] private Vector2 JumpAcceleration;

    [SerializeField] private float Force;
    [SerializeField] private float FallMultiplier;
    [SerializeField] private float LowJumpMultiplier;
    [SerializeField] private float MaintainedSpeed;

    private bool _buttonReleased = true;

    private MovementState _state;

    private void Update()
    {
        if (_state != MovementState.Move) return;
        if (Rigidbody.velocity.y < 0)
            Rigidbody.velocity += Vector2.up * (Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime);
        else if (Rigidbody.velocity.y > 0 && _buttonReleased)
            Rigidbody.velocity += Vector2.up * (Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime);

        SetSpeed(MaintainedSpeed);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        _buttonReleased = context.canceled;
        if (!context.performed) return;
        Rigidbody.velocity *= Vector2.right;
        Rigidbody.AddForce(JumpAcceleration.normalized * Force, ForceMode2D.Impulse);
    }

    public void SetState(MovementState state)
    {
        _state = state;
    }

    public MovementState GetState()
    {
        return _state;
    }

    private void SetSpeed(float newSpeed)
    {
        var verticalVelocity = Rigidbody.velocity.y;
        Rigidbody.velocity = new Vector2(newSpeed, verticalVelocity);
    }
}