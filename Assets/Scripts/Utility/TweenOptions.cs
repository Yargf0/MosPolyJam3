using DG.Tweening;
using System;

[Serializable]
public class TweenOptions
{
    public float Duration;
    public Ease Ease;

    public TweenOptions()
    {
        Duration = 0.5f;
        Ease = Ease.Linear;
    }

    public TweenOptions(float duration, Ease ease)
    {
        Duration = duration;
        Ease = ease;
    }
}