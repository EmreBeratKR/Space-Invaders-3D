using System.Collections.Generic;
using UnityEngine;
using Utils.ModularBehaviour;

namespace ShieldSystem
{
    public class Shield : MonoBehaviour, IMainBehaviour
    {
        private IEnumerable<ShieldChunk> ShieldChunks
        {
            get
            {
                if (m_ShieldChunks == null)
                {
                    m_ShieldChunks = GetComponentsInChildren<ShieldChunk>(true);
                }

                return m_ShieldChunks;
            }
        }


        private ShieldChunk[] m_ShieldChunks;


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
            RestoreChunks();
        }
        
        private void OnStartNextWave(Game.EventResponse response)
        {
            RestoreChunks();
        }
        

        private void RestoreChunks()
        {
            foreach (var shieldChunk in ShieldChunks)
            {
                shieldChunk.Restore();
            }
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
    }
}