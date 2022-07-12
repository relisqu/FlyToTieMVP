using System;
using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;

namespace DefaultNamespace.Props
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ParticleSystem;
        [SerializeField] private float TriggerDistance;
        [SerializeField] private float Speed;


        private void Update()
        {
            var position = transform.position;
            var playerPosition = StarterUnit.Instance.transform.position;
            var step = Speed * Time.deltaTime;
            if (Vector2.Distance(playerPosition, position) < TriggerDistance)
            {
                transform.position = Vector3.MoveTowards(position, playerPosition, step);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isDead && other.gameObject.TryGetComponent(out Unit _))
            {
                Die();
                PlayerData.SaveMoney(PlayerData.MoneyCount + 1);
                AudioManager.instance.Play("coin");
            }
        }

        private void OnEnable()
        {
            StopAllCoroutines();
            StartCoroutine(DieAfterTime());
        }

        public IEnumerator DieAfterTime()
        {
            yield return new WaitForSeconds(10f);
            Die();
        }

        private bool _isDead;

        public void SetAlive(bool alive)
        {
            _isDead = !alive;
        }

        public void Die()
        {
            _isDead = true;
            ParticleSystem.Emit(15);
            transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}