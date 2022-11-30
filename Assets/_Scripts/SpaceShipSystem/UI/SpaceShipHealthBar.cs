using System;
using MeshNumberSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipHealthBar : BehaviourModule<SpaceShip>, IMainBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private MeshNumber lifeCounter;
        
        
        public UnityAction<EventResponse> OnChanged;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnHealthChanged(SpaceShip.EventResponse response)
        {
            var healthLeft = response.newHealth;
            UpdateBar(healthLeft);
        }


        private void UpdateBar(int healthLeft)
        {
            var response = new EventResponse()
            {
                healthLeft = healthLeft
            };
            
            lifeCounter.Set(healthLeft);
            
            OnChanged?.Invoke(response);
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnHealthChanged += OnHealthChanged;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnHealthChanged -= OnHealthChanged;
            }
        }
        
        
        
        
        
        [Serializable]
        public struct EventResponse
        {
            public int healthLeft;
        }
    }
}