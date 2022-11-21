using UnityEngine;
using Utils;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShipMovement : BehaviourModule<SpaceShip>
    {
        [Header(Keyword.References)]
        [SerializeField] private Rigidbody body;
        
        [Header(Keyword.Values)]
        [SerializeField, Min(0f)] private float speed;
        
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        
        private void OnPerformMove(SpaceShip.EventResponse response)
        {
            var direction = response.movementDirection;

            body.velocity = Vector3.right * (direction * speed);
        }

        private void OnCancelMove(SpaceShip.EventResponse response)
        {
            body.velocity = Vector3.zero;
        }


        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnPerformMove += OnPerformMove;
                MainBehaviour.OnCancelMove += OnCancelMove;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnPerformMove -= OnPerformMove;
                MainBehaviour.OnCancelMove -= OnCancelMove;
            }
        }
    }
}