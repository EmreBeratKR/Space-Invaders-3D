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
        [SerializeField] private AudioSource dieSound;


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

        private void OnShotBySpaceShip(BonusInvader.EventResponse response)
        {
            idleSound.Stop();
            dieSound.Play();
        }

        private void OnGameOver(Game.EventResponse response)
        {
            idleSound.Stop();
        }

        private void OnWaveCleared(Game.EventResponse response)
        {
            idleSound.Stop();
        }


        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInitialized += OnInitialized;
                MainBehaviour.OnShotBySpaceShip += OnShotBySpaceShip;
            }

            Game.OnGameOver += OnGameOver;
            Game.OnWaveCleared += OnWaveCleared;
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnInitialized -= OnInitialized;
                MainBehaviour.OnShotBySpaceShip -= OnShotBySpaceShip;
            }
            
            Game.OnGameOver -= OnGameOver;
            Game.OnWaveCleared -= OnWaveCleared;
        }
    }
}