using System;
using Units;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "LaserUpgradeV2", menuName = "ScriptableObjects/UnitUpgrades/LaserShooter/V2",
        order = 0)]
    public class LaserUpgradeV2 : UnitUpgrade
    {
        public float laserScale;

        public override void UpgradeAction(Unit unit)
        {
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.OnShoot += CountShoots;
        }


        private void CountShoots(Shooter unit)
        {
            if (unit.GetShotsCount() % 3 == 0)
            {
                unit.SetProjectileScale(laserScale);
            }
            else
            {
                unit.ResetProjectileScale();
            }
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.OnShoot -= CountShoots;
        }
    }
}