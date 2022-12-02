using System;
using UnityEngine.Events;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace InvaderSystem
{
    public abstract class BonusInvader : PoolableBehaviour<BonusInvader>, IMainBehaviour, IInvader
    {
        public UnityAction<EventResponse> OnInitialized;
        public UnityAction<EventResponse> OnShotBySpaceShip;
        public UnityAction<EventResponse> OnDied;
        public UnityAction<EventResponse> OnDieAnimationComplete;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnShotBySpaceShip_Internal(EventResponse response)
        {
            TakeDamage();
        }
        
        private void OnDieAnimationComplete_Internal(EventResponse response)
        {
            Release();
        }

        private void OnDied_Internal(EventResponse response)
        {
            Game.Pause();
        }
        

        public virtual void TakeDamage()
        {
            Die();
        }

        public override void OnAfterInitialized()
        {
            OnInitialized?.Invoke(new EventResponse());
        }


        private void Die()
        {
            OnDied?.Invoke(new EventResponse());
        }
        
        protected virtual void AddListeners()
        {
            OnShotBySpaceShip += OnShotBySpaceShip_Internal;
            OnDieAnimationComplete += OnDieAnimationComplete_Internal;
            OnDied += OnDied_Internal;
        }

        protected virtual void RemoveListeners()
        {
            OnShotBySpaceShip -= OnShotBySpaceShip_Internal;
            OnDieAnimationComplete -= OnDieAnimationComplete_Internal;
            OnDied -= OnDied_Internal;
        }
        
        
        
        [Serializable]
        public struct EventResponse
        {
            
        }
    }
}