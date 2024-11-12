using UnityEngine;

public abstract class InvertableBehaviour : MonoBehaviour
{
    [Header("Invertable")]
    [SerializeField] protected bool isInverted;

    protected virtual void Start()
    {
        OnInverted();
    }

    public virtual void SetInvertable(bool invert)
    {
        if (invert == isInverted)
            return;

        isInverted = invert;
        OnInverted();
    }

    protected abstract void OnInverted();
}