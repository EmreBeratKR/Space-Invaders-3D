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
        

        protected virtual void OnHitByBullet()
        {
            Release();
        }


        private void CheckBulletHit(Collider other)
        {
            if (!other.TryGetComponent(out IBulletCollider bulletCollider)) return;
            
            OnHitByBullet();
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