using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "JumpUpgradeV2", menuName = "ScriptableObjects/UnitUpgrades/StarterUnit/V2", order = 0)]
    public class JumpUpgradeV2 : UnitUpgrade
    {
        public float lowJumpMultiplier;
        public float jumpForce;

        public override void UpgradeAction(Unit unit)
        {
            
            Debug.Log($"Applied upgrade2 for starter {Time.time}");
            var starterUnit = (StarterUnit)unit;
            PlayerMovement.Instance.EnableLowJump();
            PlayerMovement.Instance.SetJumpForce(jumpForce);
            PlayerMovement.Instance.SetLowJumpMultiplier(lowJumpMultiplier);
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            
        }
    }
}