using UnityEngine;

public abstract class BaseUIElement : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected bool disableOnStart;

    protected virtual void Start()
    {
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