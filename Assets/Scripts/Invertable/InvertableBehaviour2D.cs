using UnityEngine;

public abstract class InvertableBehaviour2D : InvertableBehaviour
{
    [Header("Glitch Effect")]
    [SerializeField] private bool spawnGlitchEffectOnStart;
    [SerializeField] private GlitchEffect glitchEffectPrefab;
    [SerializeField] private float glitchScale = 1f;

    protected GlitchEffect glitchEffectInstance;

    protected override void Start()
    {
        if (spawnGlitchEffectOnStart)
            SpawnGlitchEffect();

        base.Start();
    }

    public override void SetInvertable(bool invert)
    {
        glitchEffectInstance?.SetActive(invert);
        base.SetInvertable(invert);
    }

    protected void SpawnGlitchEffect()
    {
        glitchEffectInstance = Instantiate(glitchEffectPrefab, transform);
        glitchEffectInstance.transform.localPosition += 0.1f * Vector3.forward;
        glitchEffectInstance.SetScale(glitchScale);
        glitchEffectInstance.SetActive(false);
    }
}