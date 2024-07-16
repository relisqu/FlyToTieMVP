using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "LaserUpgradeV1", menuName = "ScriptableObjects/UnitUpgrades/LaserShooter/V1", order = 0)]
    public class LaserUpgradeV1 : UnitUpgrade
    {
        public float laserScale;

        public override void UpgradeAction(Unit unit)
        {
            
            Debug.Log($"Applied upgrade1 for laser {Time.time}");
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.SetDefaultProjectileScale(laserScale);
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            
        }
    }
}