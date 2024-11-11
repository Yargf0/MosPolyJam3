using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : BaseInvertableUIElement
{
    [Header("Health System")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("UI References")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TweenOptions tweenOptions;

    private Tween tween;

    protected override void Start()
    {
        SetHealthSystem(healthSystem);

        if (disableOnStart)
            gameObject.SetActive(false);
    }

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        if (healthSystem == null)
            return;

        this.healthSystem = healthSystem;

        UpdateView();
        healthSystem.HealthChanged += OnHealthChanged;
    }

    private void UpdateView()
    {
        float health = isInverted ? healthSystem.MaxHealth - healthSystem.Health : healthSystem.Health;
        float fillAmount = health / healthSystem.MaxHealth;

        if (fillAmount == fillImage.fillAmount)
            return;

        tween?.Kill();
        tween = fillImage.
            DOFillAmount(fillAmount, tweenOptions.Duration).
            SetEase(tweenOptions.Ease).
            OnComplete(() => gameObject.SetActive(isInverted ? fillAmount < 1f : fillAmount > 0f)).
            Play();
    }

    private void OnHealthChanged(float remainingHealth)
    {
        UpdateView();
    }

    protected override void OnInverted()
    {
        UpdateView();
    }
}