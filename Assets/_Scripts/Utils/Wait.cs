using System.Collections;
using UnityEngine.Events;
using UnityEngine;

namespace Utils
{
    public static class Wait
    {
        private const string BehaviourObjectName = "[Wait Behaviour]";
        
        
        private static WaitBehaviour Behaviour
        {
            get
            {
                if (!ms_Behaviour)
                {
                    ms_Behaviour = new GameObject(BehaviourObjectName)
                        .AddComponent<WaitBehaviour>();
                    
                    Object.DontDestroyOnLoad(ms_Behaviour);
                }

                return ms_Behaviour;
            }
        }
        
        
        private static WaitBehaviour ms_Behaviour;


        public static void ForSeconds(float delay, UnityAction action)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }
        }
        
        public static void ForSecondsRealtime(float delay, UnityAction action)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                yield return new WaitForSecondsRealtime(delay);
                action?.Invoke();
            }
        }


        private static void StartCoroutine(IEnumerator routine)
        {
            Behaviour.StartCoroutine(routine);
        }
        
        
        
        
        private class WaitBehaviour : MonoBehaviour
        {
            
        }
    }
}