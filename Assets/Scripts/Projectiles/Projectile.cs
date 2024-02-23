using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public abstract void SpawnProjectile();


        public abstract void DestroyProjectile();


    }
}