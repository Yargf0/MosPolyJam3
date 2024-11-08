//using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : BaseUIElement
{
    [Header("Health System")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("UI References")]
    [SerializeField] private Image image;
    //[SerializeField] private TweenOptions tweenOptions;

    //private Tween tween;

    protected override void Start()
    {
        base.Start();
        SetHealthSystem(healthSystem);
    }

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        if (healthSystem == null)
            return;

        healthSystem.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float remainingHealth)
    {
        float fillAmount = remainingHealth / healthSystem.MaxHealth;
        image.fillAmount = fillAmount;
        //tween?.Kill();
        //tween = image.
        //    DOFillAmount(fillAmount, tweenOptions.Duration).
        //    SetEase(tweenOptions.Ease).
        //    Play();
        //tween.onComplete += () =>
        //{
        //    if (fillAmount <= 0f)
        //        gameObject.SetActive(false);
        //};
    }
}