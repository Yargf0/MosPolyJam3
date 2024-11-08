using UnityEngine;

public abstract class PlayerModule : MonoBehaviour
{
    protected PlayerInput input;

    public virtual void Init(PlayerInput input)
    {
        this.input = input;
    }
}