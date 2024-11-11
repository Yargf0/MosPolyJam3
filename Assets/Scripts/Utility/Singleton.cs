using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject().AddComponent<T>();
                instance.name = instance.GetType().Name;
                instance.Init();
            }

            return instance;
        }

        protected set
        {
            instance = value;
        }
    }

    private static T instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
            Init();
        }
        else
            Debug.LogError($"[{nameof(Singleton<T>)}] Created two or more objects of type: {GetType().Name}");
    }

    protected virtual void Init() { }
}