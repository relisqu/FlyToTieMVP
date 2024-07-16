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
    [SerializeField] private bool LowJumpIsEnabled;
    [SerializeField] private float LowJumpMultiplier;
    [SerializeField] private float MaintainedSpeed;
    [SerializeField] private float LongJumpTriggerTime;


    [Space] [Range(0, 10)] [SerializeField]
    private float MaxVelocity;

    public static Action Jumped;

    private bool _buttonReleased = true;

    private MovementState _state;

    private float _speedCoeff = 1f;
    public static PlayerMovement Instance;
    private InputAction.CallbackContext context;
    private float jumpTime;

    public static Action OnSmallJump;
    public static Action OnBigJump;
    private void Update()
    {
        if (_state != MovementState.Move || Cutscene.IsPlayingCutscene) return;
        if (Rigidbody.velocity.y <= 0.5f)
            Rigidbody.velocity += Vector2.up * (Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime);

        if (LowJumpIsEnabled && Rigidbody.velocity.y > 0 && _buttonReleased)
            Rigidbody.velocity += Vector2.up * (Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime);


        if (context.phase == InputActionPhase.Waiting && jumpTime > 0)
        {
            if (jumpTime > LongJumpTriggerTime)
            {
                OnBigJump?.Invoke();
            }
            else
            {
                OnSmallJump?.Invoke();
            }
        }


        if (!context.canceled && context.performed)
        {
            jumpTime += Time.deltaTime;
        }
        else
        {
            jumpTime = 0;
        }

        SetSpeed(MaintainedSpeed);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (Cutscene.IsPlayingCutscene || Cutscene.LockedPlayerInputMovement) return;
        this.context = context;
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

    public void SetSpeed(float newSpeed)
    {
        _currentVelocity = newSpeed * _speedCoeff;
        var verticalVelocity = Mathf.Clamp(Rigidbody.velocity.y, -MaxVelocity, MaxVelocity);
        Rigidbody.velocity = new Vector2(_currentVelocity, verticalVelocity);
    }

    private Vector3 defaultPosition;

    public void SetSpeedCoeff(float coeff)
    {
        _speedCoeff = coeff;
    }

    public void ResetPosition()
    {
        transform.position = defaultPosition;
    }

    private void Start()
    {
        _state = MovementState.Move;
        defaultPosition = transform.position;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SetVelocity(float x, float y)
    {
        Rigidbody.velocity = new Vector2(x, y);
    }

    public void SetFallMultiplier(float fallMultiplier)
    {
        FallMultiplier = fallMultiplier;
    }

    public void EnableLowJump()
    {
        LowJumpIsEnabled = true;
    }

    public void SetJumpForce(float jumpForce)
    {
        Force = jumpForce;
    }

    public void SetLowJumpMultiplier(float lowJumpMultiplier)
    {
        LowJumpMultiplier = lowJumpMultiplier;
    }

    public void SetTriggerTimeForBigJump(float timeForBigJumpTrigger)
    {
        LongJumpTriggerTime = timeForBigJumpTrigger;
    }
}