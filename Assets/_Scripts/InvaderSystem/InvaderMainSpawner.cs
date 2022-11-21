using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace InvaderSystem
{
    public class InvaderMainSpawner : MonoBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private Transform bottomLeftTransform;
        [SerializeField] private Transform topRightTransform;
        
        [Header(Keyword.Values)]
        [SerializeField] private float spawnInterval;


        public WaitForSeconds SpawnInterval => new WaitForSeconds(spawnInterval);
        

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
        
        
        public Vector3 EvaluateGridPosition(Vector2Int gridPosition)
        {
            var gridSize = GridSize;
            var tx = gridPosition.x / (gridSize.x - 1f);
            var ty = gridPosition.y / (gridSize.y - 1f);
            
            var x = Mathf.Lerp(BottomLeftPoint.x, TopRightPoint.x, tx);
            var y = Mathf.Lerp(BottomLeftPoint.y, TopRightPoint.y, ty);
            return new Vector3(x, y, 0f);
        }


        private void Start()
        {
            SpawnAll();
        }


        private void SpawnAll()
        {
            StartCoroutine(Routine());
            

            IEnumerator Routine()
            {
                foreach (var subSpawner in SubSpawners)
                {
                    yield return subSpawner.SpawnInvaders();
                }
            }
        }
    }
}