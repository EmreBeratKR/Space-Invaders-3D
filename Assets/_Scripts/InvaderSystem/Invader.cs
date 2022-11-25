using System;
using UnityEngine.Events;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace InvaderSystem
{
    public abstract class Invader : PoolableBehaviour<Invader>, IMainBehaviour
    {
        public UnityAction<EventResponse> OnHitBySpaceShipBullet;
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


        private void OnHitBySpaceShipBullet_Internal(EventResponse response)
        {
            Die();
        }

        private void OnDieAnimationComplete_Internal(EventResponse response)
        {
            Release();
        }


        private void Die()
        {
            var response = new EventResponse()
            {

            };
            
            OnDied?.Invoke(response);
        }
        
        private void AddListeners()
        {
            OnHitBySpaceShipBullet += OnHitBySpaceShipBullet_Internal;
            OnDieAnimationComplete += OnDieAnimationComplete_Internal;
        }

        private void RemoveListeners()
        {
            OnHitBySpaceShipBullet -= OnHitBySpaceShipBullet_Internal;
            OnDieAnimationComplete -= OnDieAnimationComplete_Internal;
        }


        [Serializable]
        public struct EventResponse
        {
            
        }
    }
}