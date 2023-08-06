using System;
using DefaultNamespace.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementState
    {
        Move,
        TakeDamage,
        Die
    }

    [FormerlySerializedAs("rigidbody")] [SerializeField]
    private Rigidbody2D Rigidbody;

    [SerializeField] private Vector2 JumpAcceleration;

    [SerializeField] private float Force;
    [SerializeField] private float FallMultiplier;
    [SerializeField] private float LowJumpMultiplier;
    [SerializeField] private float MaintainedSpeed;

    [Space] [Range(0, 10)] [SerializeField]
    private float MaxVelocity;

    public static Action Jumped;

    private bool _buttonReleased = true;

    private MovementState _state;

    private void Update()
    {
        if (_state != MovementState.Move || Cutscene.IsPlayingCutscene) return;
        if (Rigidbody.velocity.y <= 0.5f)
            Rigidbody.velocity += Vector2.up * (Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime);

        if (Rigidbody.velocity.y > 0 && _buttonReleased)
            Rigidbody.velocity += Vector2.up * (Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime);

        SetSpeed(MaintainedSpeed);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (Cutscene.IsPlayingCutscene || Cutscene.LockedPlayerInputMovement) return;
        _buttonReleased = context.canceled;
        if (!context.performed) return;
        AudioManager.instance.Play("fly");
        Rigidbody.velocity *= Vector2.right;
        Jumped?.Invoke();
        Rigidbody.AddForce(JumpAcceleration.normalized * Force, ForceMode2D.Impulse);
    }

    public void Jump()
    {
        //Rigidbody.velocity *= Vector2.right;
        //Jumped?.Invoke();
        Rigidbody.AddForce(JumpAcceleration.normalized * Force, ForceMode2D.Impulse);
        SetSpeed(MaintainedSpeed);
    }

    public void DisableMovement()
    {
        _state = MovementState.Die;
        Rigidbody.gravityScale = 0;
        Rigidbody.velocity = Vector2.zero;
        SetSpeed(0f);
    }

    public void EnableMovement()
    {
        Rigidbody.gravityScale = 1;
        _state = MovementState.Move;
        SetSpeed(MaintainedSpeed);
    }


    public void SetState(MovementState state)
    {
        _state = state;
    }

    public MovementState GetState()
    {
        return _state;
    }

    private float _currentVelocity;

    private void SetSpeed(float newSpeed)
    {
        _currentVelocity = newSpeed;
        var verticalVelocity = Mathf.Clamp(Rigidbody.velocity.y, -MaxVelocity, MaxVelocity);
        Rigidbody.velocity = new Vector2(_currentVelocity, verticalVelocity);
    }

    private Vector3 defaultPosition;

    public void ResetPosition()
    {
        transform.position = defaultPosition;
    }

    private void Start()
    {
        _state = MovementState.Move;
        defaultPosition = transform.position;
    }
}