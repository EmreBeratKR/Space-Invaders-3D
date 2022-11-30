using UnityEngine;
using Utils.PoolSystem;

namespace InvaderSystem
{
    public class BonusInvaderSpawner : PoolableBehaviourSpawner<BonusInvader>
    {
        [SerializeField] private BonusInvader prefab;
        [SerializeField] private Transform spawnPointTransform;


        protected override BonusInvader Prefab => prefab;


        private Vector3 SpawnPoint => spawnPointTransform.position;
        

        public new BonusInvader Spawn()
        {
            return Spawn(SpawnPoint);
        }
    }
}