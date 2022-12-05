using SpaceShipSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SoundSystem
{
    public class SpaceShipSounds : BehaviourModule<SpaceShip>
    {
        [Header(Keyword.References)]
        [SerializeField] private AudioSource shootSound;
        [SerializeField] private AudioSource dieSound;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        

        private void OnSpaceShipTurretShootPerform(SpaceShip.EventResponse response)
        {
            shootSound.Play();
        }

        private void OnSpaceShipTakeDamage(SpaceShip.EventResponse response)
        {
            dieSound.Play();
        }


        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTurretShootPerform += OnSpaceShipTurretShootPerform;
                MainBehaviour.OnTakeDamage += OnSpaceShipTakeDamage;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTurretShootPerform -= OnSpaceShipTurretShootPerform;
                MainBehaviour.OnTakeDamage -= OnSpaceShipTakeDamage;
            }
        }
    }
}