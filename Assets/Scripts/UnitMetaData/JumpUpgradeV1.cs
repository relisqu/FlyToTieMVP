using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "JumpUpgradeV1", menuName = "ScriptableObjects/UnitUpgrades/StarterUnit/V1", order = 0)]
    public class JumpUpgradeV1 : UnitUpgrade
    {
        public float lowJumpMultiplier;
        public float jumpForce;

        public override void UpgradeAction(Unit unit)
        {
            
            Debug.Log($"Applied upgrade1 for starter {Time.time}");
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