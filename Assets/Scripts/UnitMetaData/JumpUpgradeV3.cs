using DefaultNamespace.UI;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "JumpUpgradeV2", menuName = "ScriptableObjects/UnitUpgrades/StarterUnit/V3", order = 0)]
    public class JumpUpgradeV3 : UnitUpgrade
    {
        [Range(0f, 1f)] public float TimeForBigJumpTrigger;
        public float MultiplierScale = 0.2f;


        private float scale = 1f;

       
        public override void UpgradeAction(Unit unit)
        {
            Debug.Log($"Applied upgrade3 for starter {Time.time}");
            var starterUnit = (StarterUnit)unit;
            PlayerMovement.Instance.SetTriggerTimeForBigJump(TimeForBigJumpTrigger);
            PlayerMovement.OnBigJump += AddMultiplier;
            PlayerMovement.OnSmallJump += ResetMultiplier;
            CoinMultiplierScaler.Instance.Enable();
        }

        void AddMultiplier()
        {
            scale += MultiplierScale;
            CoinMultiplierScaler.Instance.SetScale(scale);
            Debug.Log(scale);
        }

        void ResetMultiplier()
        {
            scale = 1f;
            CoinMultiplierScaler.Instance.SetScale(scale);
            Debug.Log("DROPPED " +scale);
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            PlayerMovement.OnBigJump -= AddMultiplier;
            PlayerMovement.OnSmallJump -= ResetMultiplier;
        }
    }
}