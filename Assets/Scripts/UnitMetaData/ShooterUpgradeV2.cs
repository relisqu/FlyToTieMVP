using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "ShooterUpgradeV2", menuName = "ScriptableObjects/UnitUpgrades/Shooter/V2", order = 0)]
    public class ShooterUpgradeV2 : UnitUpgrade
    {
        public float bulletScale;

        public override void UpgradeAction(Unit unit)
        {
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.SetProjectileScale(bulletScale);
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            
        }
    }
}