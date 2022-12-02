using DG.Tweening;
using UnityEngine;
using Utils;

public class GameHider : MonoBehaviour
{
    [Header(Keyword.Values)]
    [SerializeField, Min(0f)] private float duration;


    private float ShowedSize => Mathf.Abs(transform.localPosition.x * 2f);


    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
    
    
    private void OnGameOver(Game.EventResponse response)
    {
        Show();
    }
    

    private void Show()
    {
        Animate(0f, ShowedSize, 2f);
    }
    
    private void Hide()
    {
        Animate(ShowedSize, 0f);
    }

    private void Animate(float from, float to, float delay = 0f)
    {
        transform.DOScaleX(to, duration)
            .From(from)
            .SetDelay(delay)
            .SetUpdate(true);
    }

    private void AddListeners()
    {
        Game.OnGameOver += OnGameOver;
    }

    private void RemoveListeners()
    {
        Game.OnGameOver -= OnGameOver;
    }
}
