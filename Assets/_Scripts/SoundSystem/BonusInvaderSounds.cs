using InvaderSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SoundSystem
{
    public class BonusInvaderSounds : BehaviourModule<BonusInvader>
    {
        [Header(Keyword.References)]
        [SerializeField] private AudioSource idleSound;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnInitialized(BonusInvader.EventResponse response)
        {
            idleSound.Play();
        }


        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInitialized += OnInitialized;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInitialized -= OnInitialized;
            }
        }
    }
}