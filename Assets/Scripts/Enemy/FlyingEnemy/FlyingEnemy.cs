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
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float damage = 5f;

    private DefaultFlyingEnemyBehaviour defaultBehaviour;
    private InvertedFlyingEnemyBehaviour invertedBehaviour;

    private DefaultFlyingEnemyBehaviour currentBehaviour;

    protected override void Start()
    {
        defaultBehaviour = new DefaultFlyingEnemyBehaviour(this, moveSpeed, excludeLayerMask);
        invertedBehaviour = new InvertedFlyingEnemyBehaviour(this, moveSpeed, excludeLayerMask);

        base.Start();
    }

    private void Update()
    {
        currentBehaviour.Update(Time.deltaTime);
    }

    protected override void OnInverted()
    {
        currentBehaviour = isInverted ? invertedBehaviour : defaultBehaviour;
    }

    private void OnDrawGizmos()
    {
        currentBehaviour?.DrawGizmos();
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
}