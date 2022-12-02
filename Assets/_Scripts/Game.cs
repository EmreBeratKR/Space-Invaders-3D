using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public static class Game
{
    public static UnityAction<EventResponse> OnPaused;
    public static UnityAction<EventResponse> OnResumed;
    public static UnityAction<EventResponse> OnGameOver;


    [InitializeOnEnterPlayMode]
    private static void InitializeForEnterPlayMode()
    {
        OnPaused = null;
        OnResumed = null;
        OnGameOver = null;
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
    
    public static void RaiseGameOver()
    {
        OnGameOver?.Invoke(new EventResponse());
    }
    
    
    
    
    [Serializable]
    public struct EventResponse
    {
        public bool allowPlayerToPlay;
    }
}