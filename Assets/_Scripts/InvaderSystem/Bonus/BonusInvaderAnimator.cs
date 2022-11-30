using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class BonusInvaderAnimator : BehaviourModule<BonusInvader>
    {
        [Header(Keyword.References)]
        [SerializeField] private Animator animator;


        private static readonly int DieTriggerHash = Animator.StringToHash("die");


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        
        private void OnDied(BonusInvader.EventResponse response)
        {
            PlayDie();
        }


        private void PlayDie()
        {
            animator.SetTrigger(DieTriggerHash);
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnDied += OnDied;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnDied -= OnDied;
            }
        }
    }
}