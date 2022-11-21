using System;
using UnityEngine;
using UnityEngine.Events;
using Utils.ModularBehaviour;

namespace SpaceShipSystem
{
    public class SpaceShip : MonoBehaviour, IMainBehaviour
    {
        public UnityAction<EventResponse> OnPerformMove;
        public UnityAction<EventResponse> OnCancelMove;






        [Serializable]
        public struct EventResponse
        {
            public int movementDirection;
        }
    }
}