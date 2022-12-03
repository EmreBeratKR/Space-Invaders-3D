using MainMenuSystem;
using UnityEditor;
using UnityEngine.Events;

public class MainMenuLoaderTransition : SceneTransition
{
    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }


    private void OnGameStarted(Game.EventResponse response)
    {
        Hide();
    }
    
    private void OnGameOver(Game.EventResponse response)
    {
        Show(() =>
        {
            MainMenu.OnLoaded?.Invoke(new MainMenu.EventResponse());
        });
    }
    
    
    private void AddListeners()
    {
        Game.OnStarted += OnGameStarted;
        Game.OnGameOver += OnGameOver;
    }

    private void RemoveListeners()
    {
        Game.OnStarted -= OnGameStarted;
        Game.OnGameOver -= OnGameOver;
    }
}