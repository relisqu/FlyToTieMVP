using System;
using DG.Tweening;
using Player;
using UnityEngine;

namespace DefaultNamespace.Props
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ParticleSystem;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isDead && other.gameObject.TryGetComponent(out Unit _))
            {
                Die();
                PlayerData.SaveMoney(PlayerData.MoneyCount + 1);
            }
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