using UnityEngine;
using Utils.PoolSystem;

namespace BulletSystem
{
    public abstract class Bullet<T> : PoolableBehaviour<Bullet<T>>
    {
        public abstract void Shoot(T shooter, Vector3 position, Vector3 direction, float speed);
    }
}