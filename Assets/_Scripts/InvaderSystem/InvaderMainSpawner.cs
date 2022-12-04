using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace InvaderSystem
{
    public class InvaderMainSpawner : MonoBehaviour
    {
        [Header(Keyword.References)] 
        [SerializeField] private InvaderCommander commander;
        [SerializeField] private Transform bottomLeftTransform;
        [SerializeField] private Transform topRightTransform;


        public UnityAction<EventResponse> OnSpawnComplete;


        public InvaderCommander Commander => commander;


        private Vector3 BottomLeftPoint => bottomLeftTransform.position;
        private Vector3 TopRightPoint => topRightTransform.position;


        private Vector2Int GridSize
        {
            get
            {
                var result = Vector2Int.zero;

                foreach (var subSpawner in SubSpawners)
                {
                    result.x = subSpawner.GridSize.x;
                    result.y += subSpawner.GridSize.y;
                }

                return result;
            }
        }
        
        private InvaderSubSpawner[] SubSpawners
        {
            get
            {
                if (m_SubSpawners == null)
                {
                    m_SubSpawners = GetComponentsInChildren<InvaderSubSpawner>();
                }

                return m_SubSpawners;
            }
        }
        

        private InvaderSubSpawner[] m_SubSpawners;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnGameStarted(Game.EventResponse response)
        {
            SpawnAll();
        }

        private void OnStartNextWave(Game.EventResponse response)
        {
            SpawnAll();
        }
        
        
        public Vector3 EvaluateGridPosition(Vector2Int gridPosition)
        {
            var gridSize = GridSize;
            var tx = gridPosition.x / (gridSize.x - 1f);
            var ty = gridPosition.y / (gridSize.y - 1f);
            
            var x = Mathf.Lerp(BottomLeftPoint.x, TopRightPoint.x, tx);
            var y = Mathf.Lerp(BottomLeftPoint.y, TopRightPoint.y, ty);
            return new Vector3(x, y, 0f);
        }


        private void SpawnAll()
        {
            foreach (var subSpawner in SubSpawners)
            { 
                subSpawner.SpawnInvaders();
            }

            var response = new EventResponse()
            {

            };
                
            OnSpawnComplete?.Invoke(response);
        }

        private void AddListeners()
        {
            Game.OnStarted += OnGameStarted;
            Game.OnStartedNextWave += OnStartNextWave;
        }

        private void RemoveListeners()
        {
            Game.OnStarted -= OnGameStarted;
            Game.OnStartedNextWave -= OnStartNextWave;
        }
        
        
        
        [Serializable]
        public struct EventResponse
        {
            
        }
    }
}