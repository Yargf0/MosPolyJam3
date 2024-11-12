using System;
using UnityEngine;

[Serializable]
public class IB3DData
{
    public Renderer renderer;
    public Material[] normalMaterials;
    public Material[] glitchMaterials;
}

public abstract class InvertableBehaviour3D : InvertableBehaviour
{
    [Header("Glitch Effect")]
    [SerializeField] private IB3DData[] materialsData;

    protected override void Start()
    {
        ChangeMaterials();
        base.Start();
    }

    protected void ChangeMaterials()
    {
        if (isInverted)
        {
            foreach (IB3DData data in materialsData)
                data.renderer.materials = data.glitchMaterials;
        }
        else
        {
            foreach (IB3DData data in materialsData)
                data.renderer.materials = data.normalMaterials;
        }
    }
}