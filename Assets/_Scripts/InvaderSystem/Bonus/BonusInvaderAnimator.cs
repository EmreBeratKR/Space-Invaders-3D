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
            ChangeUpdateMode(AnimatorUpdateMode.UnscaledTime);
            PlayDie();
        }

        private void OnDieAnimationComplete(BonusInvader.EventResponse response)
        {
            ChangeUpdateMode(AnimatorUpdateMode.Normal);
        }


        private void PlayDie()
        {
            animator.SetTrigger(DieTriggerHash);
        }

        private void ChangeUpdateMode(AnimatorUpdateMode updateMode)
        {
            animator.updateMode = updateMode;
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnDied += OnDied;
                MainBehaviour.OnDieAnimationComplete += OnDieAnimationComplete;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnDied -= OnDied;
                MainBehaviour.OnDieAnimationComplete -= OnDieAnimationComplete;
            }
        }
    }
}