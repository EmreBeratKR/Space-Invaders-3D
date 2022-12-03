using DG.Tweening;
using UnityEngine;

public abstract class MeshButton : WritableMeshText
{
    [SerializeField, Min(0f)] private float highlightedSize;
    [SerializeField, Min(0f)] private float highlightDuration;


    private Tween m_SizeTween;
    
    
    private void OnMouseEnter()
    {
        m_SizeTween?.Kill();
        
        m_SizeTween = transform.DOScale(Vector3.one * highlightedSize, highlightDuration)
            .SetEase(Ease.OutSine)
            .SetUpdate(true);
    }

    private void OnMouseExit()
    {
        m_SizeTween?.Kill();
        
        m_SizeTween = transform.DOScale(Vector3.one, highlightDuration)
            .SetEase(Ease.OutSine)
            .SetUpdate(true);
    }

    private void OnMouseUpAsButton()
    {
        OnClicked();
    }


    protected virtual void OnClicked()
    {
        m_SizeTween?.Kill();
    }
}