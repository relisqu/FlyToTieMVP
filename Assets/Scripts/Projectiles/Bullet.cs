using System;
using System.Collections;
using DefaultNamespace.Projectile;
using UnityEngine;
using Obstacle;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float MaxLifetime;
    [SerializeField] public bool IsDestructible;
    [SerializeField] private LifeAnimator Animator;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsDestructible && other.gameObject.TryGetComponent(out Obstacle.Obstacle _))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDestructible && other.gameObject.TryGetComponent(out Obstacle.Obstacle _))
        {
            Die();
        }
    }

    private void OnEnable()
    {
        if (IsDestructible) StartCoroutine(DieAfterSomeTime());
    }

    private void Update()
    {
        var screenPosition = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0));
        if (transform.position.x > screenPosition.x + 0.3f) 
        {
            Destroy();
        }
    }

    IEnumerator DieAfterSomeTime()
    {
        yield return new WaitForSeconds(MaxLifetime);
        Die();
    }

    public virtual void Die()
    {
        StopAllCoroutines();
        Animator.PlayDeathAnimation();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        Die();
    }
}