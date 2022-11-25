using System;
using UnityEngine.Events;
using Utils.ModularBehaviour;
using Utils.PoolSystem;

namespace InvaderSystem
{
    public abstract class Invader : PoolableBehaviour<Invader>, IMainBehaviour
    {
        public UnityAction<EventResponse> OnHitBySpaceShipBullet;


        private void OnHitBySpaceShipBullet_Internal(EventResponse response)
        {
            
        }
        

        [Serializable]
        public struct EventResponse
        {
            
        }
    }
}