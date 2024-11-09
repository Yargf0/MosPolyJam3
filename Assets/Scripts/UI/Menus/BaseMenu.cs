using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMenu : BaseUIElement
{
    [Header("Controls")]
    [SerializeField] protected Button openButton;
    [SerializeField] protected Button closeButton;

    [Header("Fade Animation")]
    [SerializeField] protected Image fadeImage;
    [SerializeField] protected TweenOptions fadeTweenOptions;
    [SerializeField] protected float targetAlpha = 0.25f;

    [Header("Panel Animation")]
    [SerializeField] protected RectTransform panelTransform;
    [SerializeField] protected TweenOptions panelTweenOptions;
    [SerializeField] protected Vector2 panelStartPosition, panelEndPosition;

    protected Sequence sequence;

    protected override void Start()
    {
        base.Start();

        openButton?.onClick.AddListener(Show);
        closeButton?.onClick.AddListener(Hide);
    }

    public override void Show()
    {
        gameObject.SetActive(true);

        fadeImage.color = Color.clear;
        panelTransform.anchoredPosition = panelStartPosition;
        panelTransform.localScale = Vector3.zero;

        sequence?.Kill();
        sequence = DOTween.Sequence().
            Insert(0f, fadeImage.DOFade(targetAlpha, fadeTweenOptions.Duration).SetEase(fadeTweenOptions.Ease)).
            Insert(0f, panelTransform.DOAnchorPos(panelEndPosition, panelTweenOptions.Duration).SetEase(panelTweenOptions.Ease)).
            Insert(0f, panelTransform.DOScale(1f, panelTweenOptions.Duration).SetEase(panelTweenOptions.Ease)).
            Play();
    }

    public override void Hide()
    {
        gameObject.SetActive(false);

        sequence?.Kill();
    }
}