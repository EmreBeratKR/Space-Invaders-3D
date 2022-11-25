using UnityEngine;
using Utils;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace BulletSystem
{
    public abstract class BulletSpawner<T> : PoolableBehaviourSpawner<Bullet<T>>
        where T : IMainBehaviour
    {
        [Header(Keyword.Values)]
        [SerializeField] private Bullet<T> bulletPrefab;


        protected override Bullet<T> Prefab => bulletPrefab;
    }
}