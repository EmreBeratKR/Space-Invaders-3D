using MainMenuSystem;
using UnityEngine;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace BulletSystem
{
    public abstract class Bullet<T> : PoolableBehaviour<Bullet<T>>, IMainBehaviour
        where T : IMainBehaviour
    {
        public abstract void Shoot(T shooter, Vector3 position, Vector3 direction, float speed);


        protected virtual void OnEnable()
        {
            AddListeners();
        }

        protected virtual void OnDisable()
        {
            RemoveListeners();
        }


        protected virtual void OnTriggerEnter(Collider other)
        {
            CheckBulletHit(other);
        }

        
        private void OnMainMenuLoaded(MainMenu.EventResponse response)
        {
            Release();
        }


        private void CheckBulletHit(Collider other)
        {
            if (!other.TryGetComponent(out IHittableByBullet hittable)) return;
            
            hittable.Hit();
            Release();
        }


        private void AddListeners()
        {
            MainMenu.OnLoaded += OnMainMenuLoaded;
        }

        private void RemoveListeners()
        {
            MainMenu.OnLoaded -= OnMainMenuLoaded;
        }
    }
}