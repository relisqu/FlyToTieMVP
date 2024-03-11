using System;
using Projectiles;
using Units;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "LaserUpgradeV3", menuName = "ScriptableObjects/UnitUpgrades/LaserShooter/V3",
        order = 0)]
    public class LaserUpgradeV3 : UnitUpgrade
    {
        public float laserExplosionScale;
        public ParticleSystem laserExplosionParticle;
        public ParticleSystem.EmitParams EmitParams;

        public override void UpgradeAction(Unit unit)
        {
            var shooterUnit = (ShootingUnit)unit;
            Debug.Log($"Applied upgrade3 for laser {Time.time}");
            shooterUnit.Shooter.OnBulletHit += GenerateExplosion;
        }


        private void GenerateExplosion(Bullet unit, Vector3 shootPosition)
        {
            Debug.Log("Boom");
            var boxCollider = (BoxCollider2D)unit.GetCollider();
            var size = boxCollider.size;
            size.y *= laserExplosionScale;
            EmitParams.position = shootPosition;
            laserExplosionParticle.Emit(10000);
            var shooterParticle = Instantiate(laserExplosionParticle, shootPosition, quaternion.identity, null);
            shooterParticle.Play();
            boxCollider.size = size;
        }

        public override void ClearUpgradeActions(Unit unit)
        {
            var shooterUnit = (ShootingUnit)unit;
            shooterUnit.Shooter.OnBulletHit -= GenerateExplosion;
        }
    }
}