using System;
using MainMenuSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace InvaderSystem
{
    public abstract class Invader : PoolableBehaviour<Invader>, IMainBehaviour, IInvader
    {
        public UnityAction<EventResponse> OnInitialized;
        public UnityAction<EventResponse> OnShotBySpaceShip;
        public UnityAction<EventResponse> OnDied;
        public UnityAction<EventResponse> OnDieAnimationComplete;
        public UnityAction<EventResponse> OnShootCommand;
        public UnityAction<EventResponse> OnInvadeLowerCommand;
        public UnityAction<EventResponse> OnInvasionStep;
        public UnityAction<EventResponse> OnReachInvasionBorder;
        public UnityAction<EventResponse> OnInvasionSpeedChanged;


        public InvaderCommander Commander { get; private set; }
        public InvaderMainSpawner MainSpawner { get; private set; }
        
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

        private void OnTriggerEnter(Collider other)
        {
            CheckInvaderHit(other);
        }


        private void OnShotBySpaceShip_Internal(EventResponse response)
        {
            TakeDamage();
        }

        private void OnDieAnimationComplete_Internal(EventResponse response)
        {
            Commander.OnInvaderDied(new EventResponse());
            Release();
        }

        private void OnReachInvasionBorder_Internal(EventResponse response)
        {
            Commander.OnInvaderReachInvasionBorder(response);
        }

        private void OnDied_Internal(EventResponse response)
        {
            Game.Pause();
        }

        private void OnMainMenuLoaded(MainMenu.EventResponse response)
        {
            Release();
        }


        public override void OnAfterInitialized()
        {
            OnInitialized?.Invoke(new EventResponse());
        }

        public void HandleInvasionStep()
        {
            var response = new Invader.EventResponse()
            {
                invasionMovement = Commander.InvasionMovement
            };
            
            OnInvasionStep?.Invoke(response);
        }
        
        public void TakeDamage()
        {
            Die();
        }

        public void InjectCommander(InvaderCommander commander)
        {
            Commander = commander;
        }
        
        public void InjectMainSpawner(InvaderMainSpawner mainSpawner)
        {
            MainSpawner = mainSpawner;
        }
        
        
        private Invader NeighbourRaycast(Vector3 direction)
        {
            const float maxDistance = 100f;

            var isHit = Physics.Raycast(Position, direction, out var hitInfo, maxDistance, Physics.AllLayers, QueryTriggerInteraction.Collide);

            if (!isHit) return null;

            return hitInfo.collider.TryGetComponent(out InvaderCollider invaderCollider) ? (Invader) invaderCollider : null;
        }

        private void CheckInvaderHit(Collider other)
        {
            if (!other.TryGetComponent(out ITriggerEnterByInvader trigger)) return;
            
            trigger.TriggerEnter();
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
            OnReachInvasionBorder += OnReachInvasionBorder_Internal;
            OnDied += OnDied_Internal;
            MainMenu.OnLoaded += OnMainMenuLoaded;
        }

        private void RemoveListeners()
        {
            OnShotBySpaceShip -= OnShotBySpaceShip_Internal;
            OnDieAnimationComplete -= OnDieAnimationComplete_Internal;
            OnReachInvasionBorder -= OnReachInvasionBorder_Internal;
            OnDied -= OnDied_Internal;
            MainMenu.OnLoaded -= OnMainMenuLoaded;
        }


        [Serializable]
        public struct EventResponse
        {
            public Vector3 invasionMovement;
            public Vector3 invadeLowerMovement;
            public float invasionSpeed;
        }
    }
}