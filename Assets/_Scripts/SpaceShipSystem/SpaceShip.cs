using System;
using UnityEngine;
using UnityEngine.Events;
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
        }

        private void RemoveListeners()
        {
            OnShotByInvader += OnShotByInvader_Internal;
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