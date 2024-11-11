using DG.Tweening;
using System;
using UnityEngine;

public class HealerAttackParticle : MonoBehaviour
{
    private Action onComplete;

    public HealerAttackParticle Init(float duration, Vector3 endPosition)
    {
        transform.
            DOMove(endPosition, duration).
            SetEase(Ease.InCubic).
            OnComplete(delegate
            {
                onComplete?.Invoke();
                Destroy(gameObject);
            }).
            Play();

        return this;
    }

    public void OnComplete(Action callback)
    {
        onComplete = callback;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(-Player.Instance.LookDirection);
    }
}