using System;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipHealth : BehaviourModule<SpaceShip>
    {
        [Header(Keyword.Values)]
        [SerializeField] private int maxHealth;


        private int CurrentHealth
        {
            get => m_CurrentHealth;
            set
            {
                var oldHealth = m_CurrentHealth;
                m_CurrentHealth = value;
                var newHealth = m_CurrentHealth;

                var response = new SpaceShip.EventResponse()
                {
                    oldHealth = oldHealth,
                    newHealth = newHealth
                };
            
                MainBehaviour.OnHealthChanged?.Invoke(response);
            }
        }
        

        private int m_CurrentHealth;


        private void Start()
        {
            FullHealth();
        }


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnTakeDamage(SpaceShip.EventResponse response)
        {
            var damageTaken = response.damageTaken;
            TakeDamage(damageTaken);
        }

        private void OnDieAnimationComplete(SpaceShip.EventResponse response)
        {
            TryRespawn();
        }

        private void OnGameStarted(Game.EventResponse response)
        {
            FullHealth();
        }
        
        private void OnStartNextWave(Game.EventResponse response)
        {
            Heal(1);
        }

        
        private void FullHealth()
        {
            CurrentHealth = maxHealth;
        }

        private void TakeDamage(int damage)
        {
            Game.Pause();
            
            CurrentHealth = Mathf.Max(m_CurrentHealth - damage, 0);
        }

        private void Heal(int healAmount)
        {
            CurrentHealth = Mathf.Min(m_CurrentHealth + healAmount, maxHealth);
        }

        private bool TryRespawn()
        {
            var hasHealth = m_CurrentHealth > 0;

            if (hasHealth)
            {
                MainBehaviour.OnRespawn?.Invoke(new SpaceShip.EventResponse());
                return true;
            }
            
            MainBehaviour.OnNoHealthLeft?.Invoke(new SpaceShip.EventResponse());
            return false;
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTakeDamage += OnTakeDamage;
                MainBehaviour.OnDieAnimationComplete += OnDieAnimationComplete;
            }

            Game.OnStarted += OnGameStarted;
            Game.OnStartedNextWave += OnStartNextWave;
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTakeDamage -= OnTakeDamage;
                MainBehaviour.OnDieAnimationComplete -= OnDieAnimationComplete;
            }
            
            Game.OnStarted -= OnGameStarted;
            Game.OnStartedNextWave -= OnStartNextWave;
        }
    }
}