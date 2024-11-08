using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float offsetRadius = 2f;
    [Space(5)]
    [SerializeField] private float waitingTime = 4f;
    [Space(5)]
    [SerializeField] private LayerMask excludePlayerLayerMask;

    [Header("Attack/Heal Settings")]
    [SerializeField] private bool isInverted;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float damage = 5f;

    private Vector3 targetPoint;

    private Transform playerTransform;
    private HealthSystem playerHealth;

    private FrontFlyingEnemyBehaviour frontBehaviour;

    private void Start()
    {
        frontBehaviour = new FrontFlyingEnemyBehaviour(this, moveSpeed, excludePlayerLayerMask);

        playerTransform = Player.OriginTransform;
        playerHealth = Player.Health;

        healthSystem.Died += OnDied;
    }

    private void Update()
    {
        frontBehaviour.Update(Time.deltaTime);
    }

    private void TryAttack()
    {
        playerHealth.Damage(damage);
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }

    private void CalculateTargetPoint()
    {
        if (Vector3.Distance(targetPoint, playerTransform.position) < 5f)
            return;

        Vector3 randomPoint = Random.insideUnitSphere * offsetRadius;
        randomPoint.y = Mathf.Abs(randomPoint.y);

        targetPoint = playerTransform.position + randomPoint;
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }
}