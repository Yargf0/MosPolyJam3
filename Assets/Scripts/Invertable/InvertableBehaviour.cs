using UnityEngine;

public abstract class InvertableBehaviour : MonoBehaviour
{
    [Header("Invertable")]
    [SerializeField] protected bool isInverted;

    [Header("Glitch Effect")]
    [SerializeField] private bool spawnGlitchEffectOnStart;
    [SerializeField] private GlitchEffect glitchEffectPrefab;
    [SerializeField] private float glitchScale = 1f;

    protected GlitchEffect glitchEffectInstance;

    protected virtual void Start()
    {
        //InvertionSystem.Register(this);

        if (spawnGlitchEffectOnStart)
            SpawnGlitchEffect();

        OnInverted();
    }

    public void SetInvertable(bool invert)
    {
        if (invert == isInverted)
            return;

        isInverted = invert;
        glitchEffectInstance?.SetActive(invert);
        OnInverted();
    }

    protected void SpawnGlitchEffect()
    {
        glitchEffectInstance = Instantiate(glitchEffectPrefab, transform);
        glitchEffectInstance.transform.position += 0.1f * Vector3.forward;
        glitchEffectInstance.SetScale(glitchScale);
        glitchEffectInstance.SetActive(false);
    }

    protected abstract void OnInverted();
}