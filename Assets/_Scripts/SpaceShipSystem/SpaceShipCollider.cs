using InvaderSystem;
using UnityEngine;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipCollider : BehaviourModule<SpaceShip>, ITriggerEnterByInvader
    {
        private Collider Collider
        {
            get
            {
                if (!m_Collider)
                {
                    m_Collider = GetComponent<Collider>();
                }

                return m_Collider;
            }
        }


        private Collider m_Collider;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        public void OnShotByInvaderBullet(Invader shooter)
        {
            Collider.enabled = false;

            MainBehaviour.OnShotByInvader?.Invoke(new SpaceShip.EventResponse());
        }


        private void OnRespawn(SpaceShip.EventResponse response)
        {
            Collider.enabled = true;
        }

        private void OnGameStarted(Game.EventResponse response)
        {
            Collider.enabled = true;
        }

        
        public void TriggerEnter()
        {
            Collider.enabled = false;

            MainBehaviour.OnShotByInvader?.Invoke(new SpaceShip.EventResponse());
        }
        

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnRespawn += OnRespawn;
            }

            Game.OnStarted += OnGameStarted;
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnRespawn -= OnRespawn;
            }
            
            Game.OnStarted -= OnGameStarted;
        }
    }
}