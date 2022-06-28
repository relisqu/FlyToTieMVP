using UnityEngine;
using UnityEngine.Serialization;

public abstract class Projectile : MonoBehaviour
{
    public abstract void SpawnProjectile();


    protected abstract void DestroyProjectile();




}