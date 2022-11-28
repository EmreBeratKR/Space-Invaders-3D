using System.Collections;
using UnityEngine;
using Utils;
using Utils.PoolSystem;

namespace InvaderSystem
{
    public class InvaderSubSpawner : PoolableBehaviourSpawner<Invader>
    {
        [Header(Keyword.References)]
        [SerializeField] private Invader invaderPrefab;

        [Header(Keyword.Values)]
        [SerializeField] private Vector2Int bottomLeftGridPosition;
        [SerializeField] private Vector2Int topRightGridPosition;


        public Vector2Int GridSize => topRightGridPosition - bottomLeftGridPosition + Vector2Int.one;
        

        protected override Invader Prefab => invaderPrefab;


        private InvaderMainSpawner MainSpawner
        {
            get
            {
                if (!m_MainSpawner)
                {
                    m_MainSpawner = GetComponentInParent<InvaderMainSpawner>();
                }

                return m_MainSpawner;
            }
        }


        private InvaderMainSpawner m_MainSpawner;


        public void SpawnInvaders()
        {
            var startX = bottomLeftGridPosition.x;
            var endX = topRightGridPosition.x;
            var startY = bottomLeftGridPosition.y;
            var endY = topRightGridPosition.y;
                
            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    var gridPosition = new Vector2Int(x, y);
                    var position = MainSpawner.EvaluateGridPosition(gridPosition);
                    var newInvader = Spawn(position, Quaternion.identity, transform);
                        
                    newInvader.InjectCommander(MainSpawner.Commander);
                    newInvader.InjectMainSpawner(MainSpawner);

                }
            }
        }
    }
}