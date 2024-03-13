using System;
using DefaultNamespace;
using DefaultNamespace.Generation;
using DefaultNamespace.UI;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterUnit : Unit
{
    private static bool _isInvincible;

    [SerializeField] private ObstacleDamageable Damageable;
    [SerializeField] private Cutscene StartScene;

    protected override void OnObstacleCollision(Obstacle.Obstacle obstacle)
    {
        if (BottomUnit == this && !IsInvincible())
        {
            Animator.TakeDamage();
            CameraShake.ShakeCamera(0.3f, 20f);
            AudioManager.instance.Play("full_death");
            return;
        }

        base.OnObstacleCollision(obstacle);
    }

    public void SetAlive()
    {
        Animator.SetAlive(true);
    }

    protected override void OnEnable()
    {
    }

    public void RemoveAllChildren()
    {
        if (GetBelowUnit() == null || GetBelowUnit() == this) return;

        Destroy(GetBelowUnit().gameObject);
        BottomUnit = this;

        SetBelowUnit(null);
    }

    public void Die()
    {
        LevelGenerator.Instance.SpawnLevel(needRestart: false);
        EndCutscene.OnGameplayFinish?.Invoke();
        StartScene.PlayCutscene();
        // Animator.SetTag("Idle");
    }

    public override void OnJump()
    {
        if (Animator != null)
        {
            if (GetBelowUnit() != null)
            {
                Debug.Log("DefaultJump");
                Animator.Jump();
            }
            else
            {
                Debug.Log("SoloJump");
                Animator.Jump("SoloJump");
            }
        }
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
        SetUpdates();
        UpdateUnits += SetUpdates;
    }

    private void OnDestroy()
    {
        OnDamageTaken -= Damageable.TakeDamage;
    }

    public static StarterUnit Instance;
}