public class GameOverTitle : WritableMeshText
{
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
        Hide();
    }
    
    private void OnGameOver(Game.EventResponse response)
    {
        Show();
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