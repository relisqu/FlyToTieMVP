using System;
using DefaultNamespace;
using DefaultNamespace.UI;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterUnit : Unit
{
    private static bool _isInvincible;

    [SerializeField] private ObstacleDamageable Damageable;
    [SerializeField] private Cutscene StartScene;

    protected override void OnObstacleCollision(Obstacle obstacle)
    {
        if (BottomUnit == this && !IsInvincible())
        {
            Animator.TakeDamage();
            AudioManager.instance.Play("full_death");
            return;
        }

        base.OnObstacleCollision(obstacle);
    }

    protected override void OnEnable()
    {
    }

    public void RemoveAllChildren()
    {
        while (GetBelowUnit() != null)
        {
            Unit.BottomUnit.DestroyBottomUnit();
        }
    }

    public void Die()
    {
        StartScene.PlayCutscene();
       // Animator.SetTag("Idle");
    }

    public override void OnJump()
    {
        if (Animator != null) Animator.Jump();
    }

    public static void SetInvincible(bool value)
    {
        _isInvincible = value;
    }

    public static bool IsInvincible()
    {
        return _isInvincible;
    }
        
    private void Awake()
    {
        Instance = this;
        Instance.Damageable = GetComponent<ObstacleDamageable>();
    }

    private void Start()
    {
        SetState(UnitState.Attached);
        OnDamageTaken += Damageable.TakeDamage;
        PlayerMovement.Jumped += OnJump;
        BottomUnit = this;
    }

    private void OnDestroy()
    {
        OnDamageTaken -= Damageable.TakeDamage;
    }

    public static StarterUnit Instance;
}