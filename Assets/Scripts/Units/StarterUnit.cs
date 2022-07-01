using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterUnit : Unit
    {
        private static bool _isInvincible;

        [SerializeField] private ObstacleDamageable Damageable;

        protected override void OnObstacleCollision(Obstacle obstacle)
        {
            if (BottomUnit == this && !IsInvincible())
            {
                Animator.TakeDamage();
                return;
            }

            base.OnObstacleCollision(obstacle);
        }


        public void Die()
        {
            
            SceneManager.LoadScene("SampleScene");
        }

        public override void OnJump()
        {
            if(Animator!=null)Animator.Jump();
        }

        public static void SetInvincible(bool value)
        {
            _isInvincible = value;
        }

        public static bool IsInvincible()
        {
            return _isInvincible;
        }

        private void Start()
        {
            SetState(UnitState.Attached);
            OnDamageTaken += Damageable.TakeDamage;
            PlayerMovement.Jumped += OnJump;
            BottomUnit = this;
        }
        
    }
