using UnityEngine;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace BulletSystem
{
    public abstract class Bullet<T> : PoolableBehaviour<Bullet<T>>, IMainBehaviour
        where T : IMainBehaviour
    {
        public abstract void Shoot(T shooter, Vector3 position, Vector3 direction, float speed);


        protected virtual void OnTriggerEnter(Collider other)
        {
            CheckBulletHit(other);
        }


        private void CheckBulletHit(Collider other)
        {
            if (!other.TryGetComponent(out IBulletCollider bulletCollider)) return;
            
            Release();
        }
    }
}