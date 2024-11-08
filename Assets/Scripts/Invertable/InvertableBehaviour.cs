using UnityEngine;

public abstract class InvertableBehaviour : MonoBehaviour
{
    [Header("Invertable")]
    [SerializeField] protected bool isInverted;

    protected virtual void Start()
    {
        InvertionSystem.Register(this);
        OnInverted();
    }

    public void SetInvertable(bool invert)
    {
        if (invert == isInverted)
            return;

        OnInverted();
    }

    protected abstract void OnInverted();
}