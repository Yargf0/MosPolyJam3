using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float offsetRadius = 2f;
    [Space(5)]
    [SerializeField] private float waitingTime = 4f;
    [Space(5)]
    [SerializeField] private LayerMask excludeLayerMask;

    [Header("Attack/Heal Settings")]
    [SerializeField] private bool isInverted;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float damage = 5f;

    private Vector3 targetPoint;

    private Transform playerTransform;
    private HealthSystem playerHealth;

    private DefaultFlyingEnemyBehaviour defaultBehaviour;
    private InvertedFlyingEnemyBehaviour invertedBehaviour;

    private void Start()
    {
        defaultBehaviour = new DefaultFlyingEnemyBehaviour(this, moveSpeed, excludeLayerMask);
        invertedBehaviour = new InvertedFlyingEnemyBehaviour(this, moveSpeed, excludeLayerMask);

        playerTransform = Player.OriginTransform;
        playerHealth = Player.Health;

        healthSystem.Died += OnDied;
    }

    private void Update()
    {
        if (isInverted)
            invertedBehaviour.Update(Time.deltaTime);
        else
            defaultBehaviour.Update(Time.deltaTime);
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (isInverted)
            invertedBehaviour?.DrawGizmos();
        else
            defaultBehaviour?.DrawGizmos();
    }
}