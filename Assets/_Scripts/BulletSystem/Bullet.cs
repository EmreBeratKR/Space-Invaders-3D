using MainMenuSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace BulletSystem
{
    public abstract class Bullet<T> : PoolableBehaviour<Bullet<T>>, IMainBehaviour
        where T : IMainBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] protected Rigidbody body;
        [SerializeField] private BulletAnimator animator;
        
        
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

        private void OnStartNextWave(Game.EventResponse response)
        {
            Release();
        }


        public override void OnAfterInitialized()
        {
            animator.PlayIdle();
        }

        public virtual void OnBlast()
        {
            Stop();
            
            animator.PlayBlast(() =>
            {
                Release();
            });
        }


        protected void Stop()
        {
            body.velocity = Vector3.zero;
        }
        
        
        private void CheckBulletHit(Collider other)
        {
            if (!other.TryGetComponent(out ITriggerEnterByBullet trigger)) return;
            
            trigger.TriggerEnter();
            Release();
        }


        private void AddListeners()
        {
            MainMenu.OnLoaded += OnMainMenuLoaded;

            Game.OnStartedNextWave += OnStartNextWave;
        }

        private void RemoveListeners()
        {
            MainMenu.OnLoaded -= OnMainMenuLoaded;
            
            Game.OnStartedNextWave -= OnStartNextWave;
        }
    }
}