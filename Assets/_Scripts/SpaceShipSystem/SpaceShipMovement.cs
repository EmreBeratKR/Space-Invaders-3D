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
        [SerializeField, Min(0f)] private float movementRange;


        private Transform BodyTransform => body.transform;
        private float DeltaTime => m_IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        
        
        private bool m_IgnoreTimeScale;
        
        
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
            var movement = Vector3.right * (direction * speed * DeltaTime);
            var newPosition = BodyTransform.position + movement;
            ValidatePosition(ref newPosition);

            BodyTransform.position = newPosition;
        }

        private void OnCancelMove(SpaceShip.EventResponse response)
        {
            body.velocity = Vector3.zero;
        }

        private void OnGamePaused(Game.EventResponse response)
        {
            m_IgnoreTimeScale = response.allowPlayerToPlay;
        }

        private void OnGameResumed(Game.EventResponse response)
        {
            m_IgnoreTimeScale = false;
        }


        private void ValidatePosition(ref Vector3 position)
        {
            if (position.x > movementRange)
            {
                position.x = movementRange;
                return;
            }

            if (position.x < -movementRange)
            {
                position.x = -movementRange;
            }
        }

        private void AddListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnPerformMove += OnPerformMove;
                MainBehaviour.OnCancelMove += OnCancelMove;
                Game.OnPaused += OnGamePaused;
                Game.OnResumed += OnGameResumed;
            }
        }

        private void RemoveListeners()
        {
            if (MainBehaviour)
            {
                MainBehaviour.OnPerformMove -= OnPerformMove;
                MainBehaviour.OnCancelMove -= OnCancelMove;
                Game.OnPaused -= OnGamePaused;
                Game.OnResumed -= OnGameResumed;
            }
        }
    }
}