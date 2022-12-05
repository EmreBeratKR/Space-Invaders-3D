using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace MainMenuSystem
{
    public class MainMenu : MonoBehaviour
    {
        public static UnityAction<EventResponse> OnLoaded;


        private IEnumerable<WritableMeshText> WritableMeshTexts
        {
            get
            {
                if (m_WritableMeshTexts == null)
                {
                    m_WritableMeshTexts = GetComponentsInChildren<WritableMeshText>();
                }

                return m_WritableMeshTexts;
            }
        }


        private WritableMeshText[] m_WritableMeshTexts;


#if UNITY_EDITOR

        [InitializeOnEnterPlayMode]
        private static void InitializeForEnterPlayMode()
        {
            OnLoaded = null;
        }
        
#endif
      
        
        private void Awake()
        {
            AddListeners();
        }

        private void Start()
        {
            ShowElements();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }


        private void OnGameStarted(Game.EventResponse response)
        {
            gameObject.SetActive(false);
        }

        private void OnLoaded_Internal(EventResponse response)
        {
            Game.Resume();
            gameObject.SetActive(true);
            ShowElements();
        }


        private void ShowElements()
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                foreach (var writableMeshText in WritableMeshTexts)
                {
                    writableMeshText.Hide();
                }
                
                foreach (var writableMeshText in WritableMeshTexts)
                {
                    yield return writableMeshText.Show();
                }
            }
        }

        private void AddListeners()
        {
            Game.OnStarted += OnGameStarted;
            OnLoaded += OnLoaded_Internal;
        }

        private void RemoveListeners()
        {
            Game.OnStarted -= OnGameStarted;
            OnLoaded -= OnLoaded_Internal;
        }
        
        
        
        
        
        [Serializable]
        public struct EventResponse
        {
            
        }
    }
}