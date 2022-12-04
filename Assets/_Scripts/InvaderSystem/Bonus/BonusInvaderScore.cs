using MeshNumberSystem;
using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace InvaderSystem
{
    public class BonusInvaderScore : BehaviourModule<BonusInvader>
    {
        [Header(Keyword.References)]
        [SerializeField] private MeshNumber scoreCounter;


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
            var score = response.scoreEarned;
            scoreCounter.Set(score);
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