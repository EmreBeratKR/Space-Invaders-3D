using MeshNumberSystem;
using UnityEngine;
using Utils;

namespace WaveSystem
{
    public class WaveCounter : MonoBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private MeshNumber waveCounter;


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnWaveNumberChanged(WaveManager.EventResponse response)
        {
            var newWaveNumber = response.newWaveNumber;
            UpdateWaveCounter(newWaveNumber);
        }


        private void UpdateWaveCounter(int newWaveNumber)
        {
            waveCounter.Set(newWaveNumber);
        }

        private void AddListeners()
        {
            WaveManager.OnWaveNumberChanged += OnWaveNumberChanged;
        }

        private void RemoveListeners()
        {
            WaveManager.OnWaveNumberChanged -= OnWaveNumberChanged;
        }
    }
}