using System;
using UnityEngine;
using UnityEngine.Events;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShip : MonoBehaviour, IMainBehaviour, ISpaceShip
    {
        public UnityAction<EventResponse> OnPerformMove;
        public UnityAction<EventResponse> OnCancelMove;
        public UnityAction<EventResponse> OnTurretShoot;
        public UnityAction<EventResponse> OnShotByInvader;






        [Serializable]
        public struct EventResponse
        {
            public int movementDirection;
        }

        
        public void TakeDamage()
        {
            Debug.Log("meydey meydey!");
        }
    }
}