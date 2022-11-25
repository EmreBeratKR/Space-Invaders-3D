using System;
using UnityEngine;
using UnityEngine.Events;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace InvaderSystem
{
    public abstract class Invader : PoolableBehaviour<Invader>, IMainBehaviour, IInvader
    {
        public UnityAction<EventResponse> OnShotBySpaceShip;
        public UnityAction<EventResponse> OnDied;
        public UnityAction<EventResponse> OnDieAnimationComplete;
        public UnityAction<EventResponse> OnShootCommand;


        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Invader UpperNeighbour => NeighbourRaycast(Vector3.up);
        public Invader LowerNeighbour => NeighbourRaycast(Vector3.down);
        public Invader RightNeighbour => NeighbourRaycast(Vector3.right);
        public Invader LeftNeighbour => NeighbourRaycast(Vector3.left);


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnShotBySpaceShip_Internal(EventResponse response)
        {
            TakeDamage();
        }

        private void OnDieAnimationComplete_Internal(EventResponse response)
        {
            Release();
        }

        
        public void TakeDamage()
        {
            Die();
        }
        

        private Invader NeighbourRaycast(Vector3 direction)
        {
            const float maxDistance = 100f;

            var isHit = Physics.Raycast(Position, direction, out var hitInfo, maxDistance, Physics.AllLayers, QueryTriggerInteraction.Collide);

            if (!isHit) return null;

            return hitInfo.collider.TryGetComponent(out InvaderCollider invaderCollider) ? (Invader) invaderCollider : null;
        }
        
        private void Die()
        {
            var response = new EventResponse()
            {

            };
            
            OnDied?.Invoke(response);
        }
        
        private void AddListeners()
        {
            OnShotBySpaceShip += OnShotBySpaceShip_Internal;
            OnDieAnimationComplete += OnDieAnimationComplete_Internal;
        }

        private void RemoveListeners()
        {
            OnShotBySpaceShip -= OnShotBySpaceShip_Internal;
            OnDieAnimationComplete -= OnDieAnimationComplete_Internal;
        }


        [Serializable]
        public struct EventResponse
        {
            
        }
    }
}