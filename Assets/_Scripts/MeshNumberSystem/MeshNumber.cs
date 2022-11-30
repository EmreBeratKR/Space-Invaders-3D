using System;
using UnityEngine;
using UnityEngine.Events;
using Utils.ModularBehaviour;

namespace MeshNumberSystem
{
    public class MeshNumber : MonoBehaviour, IMainBehaviour
    {
        public UnityAction<EventResponse> OnChanged;


        public void Set(int number)
        {
            var numberAsString = number.ToString();

            var response = new EventResponse()
            {
                numberAsString = numberAsString
            };
            
            OnChanged?.Invoke(response);
        }
        
        
        
        [Serializable]
        public struct EventResponse
        {
            public string numberAsString;
        }
    }
}