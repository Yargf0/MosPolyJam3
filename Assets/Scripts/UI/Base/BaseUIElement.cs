using UnityEngine;

public class BaseUIElement : MonoBehaviour
{
    public bool IsEnabled { get; protected set; }

    [Header("Base")]
    [SerializeField] protected bool disableOnStart;

    protected virtual void Start()
    {
        if (disableOnStart)
            gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        IsEnabled = true;

        gameObject.SetActive(IsEnabled);
    }

    public virtual void Hide()
    {
        IsEnabled = false;

        gameObject.SetActive(IsEnabled);
    }
}