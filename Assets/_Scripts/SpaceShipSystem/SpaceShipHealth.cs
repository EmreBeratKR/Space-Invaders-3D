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

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTakeDamage += OnTakeDamage;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTakeDamage -= OnTakeDamage;
            }
        }
    }
}