using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = (T)this;
        else
            Debug.LogError($"[{nameof(Singleton<T>)}] Created two or more objects of type: {this.GetType().Name}");
    }
}