using System;
using ScoreSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using WaveSystem;

public static class Game
{
    public static UnityAction<EventResponse> OnStarted;
    public static UnityAction<EventResponse> OnLoaded;
    public static UnityAction<EventResponse> OnPaused;
    public static UnityAction<EventResponse> OnResumed;
    public static UnityAction<EventResponse> OnGameOver;
    public static UnityAction<EventResponse> OnWaveCleared;
    public static UnityAction<EventResponse> OnStartedNextWave;


    public static bool IsPaused => Time.timeScale == 0f;
    

    [InitializeOnEnterPlayMode]
    private static void InitializeForEnterPlayMode()
    {
        OnStarted = null;
        OnLoaded = null;
        OnPaused = null;
        OnResumed = null;
        OnGameOver = null;
        OnWaveCleared = null;
        OnStartedNextWave = null;
    }


    public static void Start()
    {
        var response = new EventResponse();
        WaveManager.OnGameStarted(response);
        ScoreManager.OnGameStarted(response);
        OnStarted?.Invoke(response);
    }

    public static void Load()
    {
        OnLoaded?.Invoke(new EventResponse());
    }
    
    public static void Pause()
    {
        Time.timeScale = 0f;
        OnPaused?.Invoke(new EventResponse()
        {
            allowPlayerToPlay = true
        });
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        OnResumed?.Invoke(new EventResponse());
    }
    
    public static void RaiseGameOver(GameOverReason reason)
    {
        ScoreManager.TryUpdateHighScore();
        
        OnGameOver?.Invoke(new EventResponse()
        {
            gameOverReason = reason
        });
    }

    public static void RaiseWaveCleared()
    {
        Pause();
        OnWaveCleared?.Invoke(new EventResponse());
    }
    
    public static void StartNextWave()
    {
        var response = new EventResponse();
        WaveManager.OnStartNextWave(response);
        Resume();
        OnStartedNextWave?.Invoke(response);
    }
    
    
    
    public enum GameOverReason
    {
        NoHealthLeft,
        InvaderReachedBase
    }
    
    [Serializable]
    public struct EventResponse
    {
        public GameOverReason gameOverReason;
        public bool allowPlayerToPlay;
    }
}