using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TweenOptions tweenOptions;

    private void Start()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        Tween tween = image.DOFade(0f, tweenOptions.Duration).SetEase(tweenOptions.Ease).Play();
        tween.onComplete += () => image.gameObject.SetActive(false);
    }
}