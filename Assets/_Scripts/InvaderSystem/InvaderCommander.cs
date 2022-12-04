using System;
using System.Collections;
using MainMenuSystem;
using SoundSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

namespace InvaderSystem
{
    public class InvaderCommander : MonoBehaviour
    {
        [Header(Keyword.References)] 
        [SerializeField] private InvasionSound invasionSound;
        [SerializeField] private InvaderMainSpawner mainSpawner;
        [SerializeField] private BonusInvaderMainSpawner bonusMainSpawner;

        [Header(Keyword.Values)] 
        [SerializeField, Min(0f)] private float invadeLowerSpeed;
        [SerializeField, Min(0f)] private float rateOfFirePerSecond;
        [SerializeField, Min(0f)] private AnimationCurve invasionSpeedCurve;
        [SerializeField, Min(0f)] private float maxInvasionSpeed;
        [SerializeField, Min(0f)] private float minInvasionSpeed;
        [SerializeField, Min(0f)] private float invasionSpeed;
        [SerializeField] private RangeFloat ufoSpawnIntervalRange;


        public UnityAction<EventResponse> OnInvasionBegin;


        public Vector3 InvasionMovement => m_InvasionDirection * invasionSpeed;
        public Vector3 InvadeLowerMovement => Vector3.down * invadeLowerSpeed;
        

        private Invader[] Invaders => GetComponentsInChildren<Invader>();

        private Invader RandomInvader
        {
            get
            {
                var invaders = Invaders;
                var randomIndex = Random.Range(0, invaders.Length);
                return invaders.Length == 0 ? null : invaders[randomIndex];
            }
        }


        private Vector3 m_InvasionDirection;
        private float m_InvasionSpeed;
        private int m_StartInvaderCount;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnWaveCleared(Game.EventResponse response)
        {
            DeInitialize();
        }
        
        private void OnMainMenuLoaded(MainMenu.EventResponse response)
        {
            DeInitialize();
        }
        
        private void OnInvaderSpawnComplete(InvaderMainSpawner.EventResponse response)
        {
            Initialize();
        }

        public void OnInvaderReachInvasionBorder(Invader.EventResponse response)
        {
            CommandInvadeLower();
            InvertDirection();
        }

        public void OnInvaderDied(Invader.EventResponse response)
        {
            
        }

        public void OnInvaderReleased(Invader.EventResponse response)
        {
            UpdateInvasionSpeed();
            CheckIfAllInvadersDied();
        }
        

        private void Initialize()
        {
            CacheStartInvaderCount();
            
            Wait.ForSecondsRealtime(1f, () =>
            {
                CommandShoot();
                StartInvasion();
                CommandUfoSpawn();
            });
        }

        private void DeInitialize()
        {
            StopAllCoroutines();
        }

        private void CheckIfAllInvadersDied()
        {
            var invaders = Invaders;
            var invaderCount = invaders.Length;
            
            if (invaderCount > 0) return;
            
            Game.RaiseWaveCleared();
        }
        
        private void CacheStartInvaderCount()
        {
            var invaders = Invaders;
            m_StartInvaderCount = invaders.Length;
        }
        
        private void CommandShoot()
        {
            StartCoroutine(Routine());
            
            IEnumerator Routine()
            {
                var interval = new WaitForSeconds(1f / rateOfFirePerSecond);

                while (true)
                {
                    yield return interval;

                    var randomInvader = RandomInvader;
                    
                    if (!randomInvader) yield break;
                    
                    while (true)
                    {
                        var lowerNeighbour = randomInvader.LowerNeighbour;
                        
                        if (!lowerNeighbour) break;

                        randomInvader = lowerNeighbour;
                    }

                    var response = new Invader.EventResponse()
                    {

                    };

                    randomInvader.OnShootCommand?.Invoke(response);
                }
            }
        }

        private void CommandInvadeLower()
        {
            var invaders = Invaders;
            var response = new Invader.EventResponse()
            {
                invadeLowerMovement = InvadeLowerMovement
            };

            foreach (var invader in invaders)
            {
                invader.OnInvadeLowerCommand?.Invoke(response);
            }
        }

        private void CommandUfoSpawn()
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                while (true)
                {
                    var randomInterval = ufoSpawnIntervalRange.Random;
                    yield return new WaitForSeconds(randomInterval);
                    bonusMainSpawner.SpawnUfo();
                }
            }
        }

        private void StartInvasion()
        {
            m_InvasionDirection = Vector3.right;
            UpdateInvasionSpeed();

            OnInvasionBegin?.Invoke(new EventResponse());
        }

        private void InvertDirection()
        {
            m_InvasionDirection *= -1f;
        }
        
        private void UpdateInvasionSpeed()
        {
            var invaders = Invaders;
            var currentInvaderCount = invaders.Length;
            var deadInvaderCount = m_StartInvaderCount - currentInvaderCount;
            var deadInvaderRate = deadInvaderCount / (m_StartInvaderCount - 1f);
            m_InvasionSpeed = CalculateInvasionStepSpeed(deadInvaderRate);

            var response = new Invader.EventResponse()
            {
                invasionSpeed = m_InvasionSpeed
            };
            
            foreach (var invader in invaders)
            {
                invader.OnInvasionSpeedChanged?.Invoke(response);
            }
            
            invasionSound.UpdateTempo(deadInvaderRate);
        }

        private float CalculateInvasionStepSpeed(float t)
        {
            var evaluatedT = invasionSpeedCurve.Evaluate(t);
            return Mathf.Lerp(minInvasionSpeed, maxInvasionSpeed, evaluatedT);
        }

        private void AddListeners()
        {
            if (mainSpawner)
            {
                mainSpawner.OnSpawnComplete += OnInvaderSpawnComplete;
            }

            Game.OnWaveCleared += OnWaveCleared;
            
            MainMenu.OnLoaded += OnMainMenuLoaded;
        }

        private void RemoveListeners()
        {
            if (mainSpawner)
            {
                mainSpawner.OnSpawnComplete -= OnInvaderSpawnComplete;
            }
            
            Game.OnWaveCleared -= OnWaveCleared;
            
            MainMenu.OnLoaded -= OnMainMenuLoaded;
        }
        
        
        
        
        [Serializable]
        public struct EventResponse
        {
            
        }
    }
}