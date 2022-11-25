using UnityEngine;
using Utils;
using Utils.PoolSystem;

namespace BulletSystem
{
    public abstract class BulletSpawner<T> : PoolableBehaviourSpawner<Bullet<T>>
    {
        [Header(Keyword.Values)]
        [SerializeField] private Bullet<T> bulletPrefab;


        protected override Bullet<T> Prefab => bulletPrefab;
    }
}