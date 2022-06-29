using Player;
using UnityEngine;

    public class StarterUnit : Unit
    {
        private static bool _isInvisible;

        [SerializeField] private ObstacleDamageable Damageable;

        protected override void OnObstacleCollision(Obstacle obstacle)
        {
            Animator.TakeDamage();
            if (BottomUnit == this)
            {
                Debug.Log("Lost");
                return;
            }

            base.OnObstacleCollision(obstacle);
        }


        public override void OnJump()
        {
            Animator.Jump();
        }

        public static void SetInvisible(bool value)
        {
            _isInvisible = value;
        }

        public static bool IsInvisible()
        {
            return _isInvisible;
        }

        private void Start()
        {
            SetState(UnitState.Attached);
            OnDamageTaken += Damageable.TakeDamage;
            PlayerMovement.Jumped += OnJump;
            BottomUnit = this;
        }
        
    }
