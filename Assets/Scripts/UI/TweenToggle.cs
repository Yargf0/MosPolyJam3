using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TweenToggle : MonoBehaviour,
    IPointerDownHandler
{
    [SerializeField] private Image targetGraphic;

    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    [SerializeField] private TweenOptions tweenOptions;

    private Sequence sequence;

    public Observer<bool> IsOn { get; private set; } = new(true);

    private void Awake()
    {
        IsOn.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(bool prevValue, bool newValue)
    {
        if (!gameObject.activeInHierarchy)
            targetGraphic.sprite = IsOn.Value ? onSprite : offSprite;
        else
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.
                Append(targetGraphic.DOFade(0.75f, tweenOptions.Duration).SetEase(tweenOptions.Ease)).
                Append(targetGraphic.DOFade(1f, tweenOptions.Duration).SetEase(tweenOptions.Ease)).
                OnComplete(() => targetGraphic.sprite = IsOn.Value ? onSprite : offSprite).
                Play();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsOn.Value = !IsOn.Value;
    }
}