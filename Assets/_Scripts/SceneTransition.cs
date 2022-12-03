using DG.Tweening;
using UnityEngine;
using Utils;

public abstract class SceneTransition : MonoBehaviour
{
    [Header(Keyword.Values)]
    [SerializeField, Min(0f)] private float duration;
    [SerializeField, Min(0f)] private float delay;


    private float ShowedSize => Mathf.Abs(transform.localPosition.x * 2f);


    protected void Show(TweenCallback callback = null)
    {
        Animate(0f, ShowedSize, delay, callback);
    }
    
    protected void Hide(TweenCallback callback = null)
    {
        Animate(ShowedSize, 0f, 0f, callback);
    }
    

    private void Animate(float from, float to, float tweenDelay = 0f, TweenCallback callback = null)
    {
        transform.DOScaleX(to, duration)
            .From(from)
            .SetEase(Ease.OutSine)
            .SetDelay(tweenDelay)
            .SetUpdate(true)
            .onComplete = callback;
    }
}
