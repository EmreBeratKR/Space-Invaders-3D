using Utils;

public class GameLoaderTransition : SceneTransition
{
    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }


    private void OnGameLoaded(Game.EventResponse response)
    {
        StartGameAfterLoad();
    }

    private void OnGameStarted(Game.EventResponse response)
    {
        Hide();
    }

    private void OnWaveCleared(Game.EventResponse response)
    {
        StartNextWaveAfterLoad();
    }
    
    private void OnStartedNextWave(Game.EventResponse response)
    {
        Hide();
    }


    private void StartGameAfterLoad()
    {
        Show(Game.Start);
    }

    private void StartNextWaveAfterLoad()
    {
        Show(() =>
        {
            Wait.ForSecondsRealtime(0.5f, () =>
            {
                Game.StartNextWave();
            });
        });
    }
    
    private void AddListeners()
    {
        Game.OnLoaded += OnGameLoaded;
        Game.OnStarted += OnGameStarted;
        Game.OnWaveCleared += OnWaveCleared;
        Game.OnStartedNextWave += OnStartedNextWave;
    }

    private void RemoveListeners()
    {
        Game.OnLoaded -= OnGameLoaded;
        Game.OnStarted -= OnGameStarted;
        Game.OnWaveCleared -= OnWaveCleared;
        Game.OnStartedNextWave -= OnStartedNextWave;
    }
}