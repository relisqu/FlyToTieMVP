using UnityEngine;
using Obstacle;
using Scripts.Obstacle;

namespace Player
{
    [CreateAssetMenu(fileName = "ShooterUpgradeV3", menuName = "ScriptableObjects/UnitUpgrades/Shooter/V3", order = 1)]
    public class ShooterUpgradeV3 : UnitUpgrade
    {
        public float GenerationChance;
        public int MinCoinCount;
        public int MaxCoinCount;

        public override void UpgradeAction(Unit unit)
        {
            Debug.Log("Set money for shooter upgrade");
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.OnBulletEnemyDeath += GenerateMoney;
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.OnBulletEnemyDeath -= GenerateMoney;
        }

        public void GenerateMoney(Vector3 position)
        {
            Debug.Log("GENERATED MONEY");
            CoinSpawner.GenerateCoinsAtPoint(MinCoinCount, MaxCoinCount, GenerationChance, position);
        }
    }
}