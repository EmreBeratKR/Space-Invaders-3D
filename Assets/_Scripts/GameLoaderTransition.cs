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


    private void StartGameAfterLoad()
    {
        Show(Game.Start);
    }
    
    private void AddListeners()
    {
        Game.OnLoaded += OnGameLoaded;
        Game.OnStarted += OnGameStarted;
    }

    private void RemoveListeners()
    {
        Game.OnLoaded -= OnGameLoaded;
        Game.OnStarted -= OnGameStarted;
    }
}