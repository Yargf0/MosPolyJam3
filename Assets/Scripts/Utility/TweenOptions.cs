using DG.Tweening;
using System;

[Serializable]
public class TweenOptions
{
    public float Duration = 0.5f;
    public Ease Ease = Ease.Linear;

    public TweenOptions(float duration, Ease ease)
    {
        Duration = duration;
        Ease = ease;
    }
}