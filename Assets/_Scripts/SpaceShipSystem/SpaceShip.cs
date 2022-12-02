using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShip : MonoBehaviour, IMainBehaviour, ISpaceShip
    {
        public UnityAction<EventResponse> OnPerformMove;
        public UnityAction<EventResponse> OnCancelMove;
        public UnityAction<EventResponse> OnTurretShoot;
        public UnityAction<EventResponse> OnShotByInvader;
        public UnityAction<EventResponse> OnTakeDamage;
        public UnityAction<EventResponse> OnHealthChanged;
        public UnityAction<EventResponse> OnNoHealthLeft;
        public UnityAction<EventResponse> OnDieAnimationComplete;
        public UnityAction<EventResponse> OnRespawn;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnShotByInvader_Internal(EventResponse response)
        {
            TakeDamage();
        }

        private void OnRespawn_Internal(EventResponse response)
        {
            Game.Resume();
        }

        private void OnNoHealthLeft_Internal(EventResponse response)
        {
            Game.RaiseGameOver();
        }
        
        
        public void TakeDamage()
        {
            var response = new EventResponse()
            {
                damageTaken = 1
            };
            
            OnTakeDamage?.Invoke(response);
        }


        private void AddListeners()
        {
            OnShotByInvader += OnShotByInvader_Internal;
            OnRespawn += OnRespawn_Internal;
            OnNoHealthLeft += OnNoHealthLeft_Internal;
        }

        private void RemoveListeners()
        {
            OnShotByInvader += OnShotByInvader_Internal;
            OnRespawn -= OnRespawn_Internal;
            OnNoHealthLeft -= OnNoHealthLeft_Internal;
        }






        [Serializable]
        public struct EventResponse
        {
            public int movementDirection;
            public int damageTaken;
            public int oldHealth;
            public int newHealth;
        }
    }
}