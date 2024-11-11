using UnityEngine;

public class AngelEnemyConfig : ScriptableObject
{
    [Header("Move Settigns")]
    public float offset = 3f;
    public LayerMask excludeInMovementLayerMask;

    [Header("Hang On Settings")]
    public float hangOnSpeed = 1f;
    public float hangOnRadius = 3f;
    public MinMaxValue<float> hangOnTime = new(1f, 5f);

    [Header("Chase Settings")]
    public float chaseSpeed = 5f;
    public float chaseDistance = 5f;
    public MinMaxValue<float> chaseAngle = new(-20, 20);

    [Header("Attack Settings")]
    public float damage = 1f;
    public float attackDistance = 4f;
    public float attackOverlapRadius = 1f;
    public LayerMask playerLayerMask;
    [Space(10)]
    public MinMaxValue<float> attackPreparingTime = new(1f, 4f);
    public MinMaxValue<float> attackTime = new(1f, 4f);
    [Space(10)]
    public AngelAttackParticle attackParticle;
    public ParticleSystem.MinMaxGradient attackParticleColor = new(Color.yellow);
}