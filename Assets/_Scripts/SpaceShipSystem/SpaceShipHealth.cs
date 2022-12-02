using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipHealth : BehaviourModule<SpaceShip>
    {
        [Header(Keyword.Values)]
        [SerializeField] private int maxHealth;


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

        
        private void FullHealth()
        {
            m_CurrentHealth = maxHealth;

            var response = new SpaceShip.EventResponse()
            {
                oldHealth = maxHealth,
                newHealth = maxHealth
            };
            
            MainBehaviour.OnHealthChanged?.Invoke(response);
        }

        private void TakeDamage(int damage)
        {
            Game.Pause();
            
            var oldHealth = m_CurrentHealth;
            m_CurrentHealth = Mathf.Max(0, m_CurrentHealth - damage);
            var newHealth = m_CurrentHealth;

            var response = new SpaceShip.EventResponse()
            {
                oldHealth = oldHealth,
                newHealth = newHealth
            };
            
            MainBehaviour.OnHealthChanged?.Invoke(response);
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
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTakeDamage -= OnTakeDamage;
                MainBehaviour.OnDieAnimationComplete -= OnDieAnimationComplete;
            }
        }
    }
}