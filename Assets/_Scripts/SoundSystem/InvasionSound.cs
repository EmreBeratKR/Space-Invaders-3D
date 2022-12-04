using System.Collections;
using UnityEngine;
using Utils;

namespace SoundSystem
{
    public class InvasionSound : MonoBehaviour
    {
        [Header(Keyword.References)] 
        [SerializeField] private RangeFloat tempoRange;
        [SerializeField] private AnimationCurve tempoCurve;


        private AudioSource[] AudioSources
        {
            get
            {
                if (m_AudioSources == null)
                {
                    m_AudioSources = GetComponentsInChildren<AudioSource>();
                }

                return m_AudioSources;
            }
        }
        private AudioSource CurrentAudioSource => AudioSources[m_CurrentAudioSourceIndex];
        private int AudioSourceCount => AudioSources.Length;


        private AudioSource[] m_AudioSources;
        private int m_CurrentAudioSourceIndex;
        private float m_Tempo;
        

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
            StartPlaying();
        }

        private void OnStartedNextWave(Game.EventResponse response)
        {
            StartPlaying();
        }

        private void OnGameOver(Game.EventResponse response)
        {
            StopPlaying();
        }

        private void OnWaveCleared(Game.EventResponse response)
        {
            StopPlaying();
        }


        public void UpdateTempo(float t)
        {
            var evaluatedT = tempoCurve.Evaluate(t);
            m_Tempo = tempoRange.Lerp(evaluatedT);
        }
        
        
        private void StartPlaying()
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                ResetTempo();

                while (true)
                {
                    yield return new WaitForSecondsRealtime(1f / m_Tempo);
                    Play();
                }
            }
        }

        private void StopPlaying()
        {
            StopAllCoroutines();
        }
        
        private void Play()
        {
            CurrentAudioSource.Play();
            m_CurrentAudioSourceIndex = (m_CurrentAudioSourceIndex + 1) % AudioSourceCount;
        }

        private void ResetTempo()
        {
            m_Tempo = tempoRange.min;
        }

        private void AddListeners()
        {
            Game.OnStarted += OnGameStarted;
            Game.OnStartedNextWave += OnStartedNextWave;
            Game.OnGameOver += OnGameOver;
            Game.OnWaveCleared += OnWaveCleared;
        }

        private void RemoveListeners()
        {
            Game.OnStarted -= OnGameStarted;
            Game.OnStartedNextWave -= OnStartedNextWave;
            Game.OnGameOver -= OnGameOver;
            Game.OnWaveCleared -= OnWaveCleared;
        }
    }
}