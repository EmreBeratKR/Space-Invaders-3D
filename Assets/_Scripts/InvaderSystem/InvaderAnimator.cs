using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class InvaderAnimator : BehaviourModule<Invader>
    {
        [Header(Keyword.References)]
        [SerializeField] private Animator animator;


        private static readonly int DieTriggerHash = Animator.StringToHash("die");
        private static readonly int InvasionSpeedFloatHash = Animator.StringToHash("invasionSpeed");


        private float InvasionSpeed
        {
            get => animator.GetFloat(InvasionSpeedFloatHash);
            set => animator.SetFloat(InvasionSpeedFloatHash, value);
        }


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnDied(Invader.EventResponse response)
        {
            ChangeUpdateMode(AnimatorUpdateMode.UnscaledTime);
            PlayDie();
        }

        private void OnDieAnimationComplete(Invader.EventResponse response)
        {
            ChangeUpdateMode(AnimatorUpdateMode.Normal);
        }

        private void OnInvasionSpeedChanged(Invader.EventResponse response)
        {
            var newInvasionSpeed = response.invasionSpeed;
            InvasionSpeed = newInvasionSpeed;
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
                MainBehaviour.OnInvasionSpeedChanged += OnInvasionSpeedChanged;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnDied -= OnDied;
                MainBehaviour.OnDieAnimationComplete -= OnDieAnimationComplete;
                MainBehaviour.OnInvasionSpeedChanged -= OnInvasionSpeedChanged;
            }
        }
    }
}