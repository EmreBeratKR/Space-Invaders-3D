using System;
using UnityEditor;
using UnityEngine.Events;

namespace WaveSystem
{
    public static class WaveManager
    {
        private const int DefaultWaveNumber = 1;
        
        
        public static int WaveSpawnOffset => WaveSpawnTable[WaveIndex];
        
        
        private static int WaveIndex => (ms_WaveNumber - 1) % WaveSpawnTable.Length;

        private static int WaveNumber
        {
            get => ms_WaveNumber;
            set
            {
                var oldWaveNumber = ms_WaveNumber;
                var newWaveNumber = value;
                ms_WaveNumber = newWaveNumber;
                OnWaveNumberChanged?.Invoke(new EventResponse()
                {
                    oldWaveNumber = oldWaveNumber,
                    newWaveNumber = newWaveNumber
                });
            }
        }


        public static UnityAction<EventResponse> OnWaveNumberChanged;


        private static readonly int[] WaveSpawnTable = new int[]
        {
            0, 1, 2, 3, 4, 5, 6, 6
        };
        
        
        private static int ms_WaveNumber;

        
        [InitializeOnEnterPlayMode]
        private static void InitializeForEnterPlayMode()
        {
            OnWaveNumberChanged = null;
        }
        

        public static void OnGameStarted(Game.EventResponse response)
        {
            ResetWaveNumber();
        }

        public static void OnStartNextWave(Game.EventResponse response)
        {
            IncrementWaveNumber();
        }


        private static void ResetWaveNumber()
        {
            WaveNumber = DefaultWaveNumber;
        }

        private static void IncrementWaveNumber()
        {
            WaveNumber++;
        }
        
        
        
        
        [Serializable]
        public struct EventResponse
        {
            public int oldWaveNumber;
            public int newWaveNumber;
        }
    }
}