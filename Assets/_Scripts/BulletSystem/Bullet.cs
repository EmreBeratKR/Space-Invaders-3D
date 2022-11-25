using UnityEngine;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace BulletSystem
{
    public abstract class Bullet<T> : PoolableBehaviour<Bullet<T>>, IMainBehaviour
        where T : IMainBehaviour
    {
        public abstract void Shoot(T shooter, Vector3 position, Vector3 direction, float speed);
    }
}