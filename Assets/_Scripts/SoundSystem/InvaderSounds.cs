using InvaderSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SoundSystem
{
    public class InvaderSounds : BehaviourModule<Invader>
    {
        [Header(Keyword.References)]
        [SerializeField] private AudioSource dieSound;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnShotBySpaceShip(Invader.EventResponse response)
        {
            dieSound.Play();
        }


        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnShotBySpaceShip += OnShotBySpaceShip;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnShotBySpaceShip -= OnShotBySpaceShip;
            }
        }
    }
}