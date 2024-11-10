using UnityEngine;

public abstract class BaseInvertableUIElement : InvertableBehaviour
{
    [Header("Base")]
    [SerializeField] protected bool disableOnStart;

    protected override void Start()
    {
        base.Start();

        if (disableOnStart)
            gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}