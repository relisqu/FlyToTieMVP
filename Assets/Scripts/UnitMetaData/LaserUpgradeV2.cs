using System;
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
                Debug.Log("Tried to shoot big one " +unit.name+ unit.GetShotsCount());
                unit.SetProjectileScale(laserScale * 4);
            }
            else
            {
                Debug.Log("Tried to shoot small one"  +unit.name+ unit.GetShotsCount());
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