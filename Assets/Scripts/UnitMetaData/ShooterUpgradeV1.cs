using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "ShooterUpgradeV1", menuName = "ScriptableObjects/UnitUpgrades/Shooter/V1", order = 0)]
    public class ShooterUpgradeV1 : UnitUpgrade
    {
        public float bulletSpeed;

        public override void UpgradeAction(Unit unit)
        {
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.SetProjectileSpeed(bulletSpeed);
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            
        }
    }
}