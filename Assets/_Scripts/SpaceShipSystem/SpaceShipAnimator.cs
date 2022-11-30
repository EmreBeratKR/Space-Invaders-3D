using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipAnimator : BehaviourModule<SpaceShip>
    {
        [Header(Keyword.References)]
        [SerializeField] private Animator animator;


        private static readonly int DieTriggerHash = Animator.StringToHash("die");
        private static readonly int IdleTriggerHash = Animator.StringToHash("idle");


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }
        

        private void OnTakeDamage(SpaceShip.EventResponse response)
        {
            PlayDie();
        }

        private void OnRespawn(SpaceShip.EventResponse response)
        {
            PlayIdle();
        }
        
        
        private void PlayIdle()
        {
            animator.SetTrigger(IdleTriggerHash);
        }
        
        private void PlayDie()
        {
            animator.SetTrigger(DieTriggerHash);
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTakeDamage += OnTakeDamage;
                MainBehaviour.OnRespawn += OnRespawn;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnTakeDamage -= OnTakeDamage;
                MainBehaviour.OnRespawn -= OnRespawn;
            }
        }
    }
}