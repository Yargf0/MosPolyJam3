using UnityEngine;

public class AngelEnemy : BaseEnemy
{
    [Header("Configs")]
    [SerializeField] private AngelEnemyConfig defaultBehaviourConfig;
    [SerializeField] private AngelEnemyConfig invertedBehaviourConfig;

    private DefaultAngelBehaviour defaultBehaviour;
    private InvertedAngelBehaviour invertedBehaviour;

    private DefaultAngelBehaviour currentBehaviour;

    protected override void Start()
    {
        defaultBehaviour ??= new DefaultAngelBehaviour(this, defaultBehaviourConfig, 0.5f);
        invertedBehaviour ??= new InvertedAngelBehaviour(this, invertedBehaviourConfig, 0.5f);

        base.Start();
    }

    private void Update()
    {
        currentBehaviour.Update(Time.deltaTime);
    }

    protected override void OnInverted()
    {
        currentBehaviour = isInverted ? invertedBehaviour : defaultBehaviour;
        glitchEffectInstance.SetActive(isInverted);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerVelocity = other.attachedRigidbody.velocity;
            playerVelocity.x = playerVelocity.z = 0f;

            other.attachedRigidbody.velocity = playerVelocity;
        }
    }

    private void DrawGizmos(DefaultAngelBehaviour behaviour, AngelEnemyConfig config)
    {
        if (behaviour == null || !behaviour.isChasing)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(behaviour == null ? transform.position : behaviour.targetPosition, config.hangOnRadius);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, config.chaseDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.attackDistance);

        if (behaviour != null && behaviour.attackTimer.IsPlaying)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(behaviour.attackPosition, config.attackOverlapRadius);
        }
    }

    private void OnDrawGizmos()
    {
        if (isInverted)
            DrawGizmos(invertedBehaviour, invertedBehaviourConfig);
        else
            DrawGizmos(defaultBehaviour, defaultBehaviourConfig);
    }
}