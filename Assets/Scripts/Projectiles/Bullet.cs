using System;
using System.Collections;
using DefaultNamespace.Projectile;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float MaxLifetime;
    [SerializeField] public bool IsDestructible;
    [SerializeField] private LifeAnimator Animator;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsDestructible && other.gameObject.TryGetComponent(out Obstacle _))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDestructible && other.gameObject.TryGetComponent(out Obstacle _))
        {
            Die();
        }
    }

    private void OnEnable()
    {
        if (IsDestructible) StartCoroutine(DieAfterSomeTime());
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