using UnityEngine;

public class DefaultFlyingEnemyBehaviour
{
    protected float moveSpeed;

    protected float offset = 3f;
    protected Vector3 targetPosition;

    protected float hangOnSpeed = 1f;
    protected float hangOnRadius = 3f;
    protected MinMaxValue<float> hangOnTime = new(1f, 5f);
    protected CountdownTimer hangOnTimer;
    protected Vector3 hangOnPosition;

    protected float chaseSpeed;
    protected float chaseDistance = 5f;
    protected bool isChasing;

    protected float bodyRadius;
    protected LayerMask excludeLayerMask;

    protected float damage = 1f;
    protected float attackDistance;
    protected MinMaxValue<float> attackPreparingTime = new(1f, 4f);
    protected MinMaxValue<float> attackTime = new(1f, 4f);
    protected CountdownTimer attackTimer;
    protected CountdownTimer attackPreparingTimer;

    protected Vector3 attackPosition;
    protected float attackOverlapRadius = 1f;

    protected FlyingEnemy owner;
    protected Transform playerTransform;

    protected Transform transform => owner.transform;
    protected float randomAttackPreparingTime => Random.Range(attackPreparingTime.min, attackPreparingTime.max);
    protected float randomAttackTime => Random.Range(attackTime.min, attackTime.max);
    protected float randomHangOnTime => Random.Range(hangOnTime.min, hangOnTime.max);

    public DefaultFlyingEnemyBehaviour(FlyingEnemy owner, float chaseSpeed, LayerMask excludePlayerLayerMask)
    {
        this.chaseSpeed = chaseSpeed;
        this.bodyRadius = 0.5f; // TODO: get from owner
        this.excludeLayerMask = excludePlayerLayerMask;

        this.owner = owner;
        this.playerTransform = Player.OriginTransform;

        moveSpeed = hangOnSpeed;
        targetPosition = transform.position;

        hangOnTimer = new CountdownTimer(0f);

        attackDistance = chaseDistance - 1f;
        attackPreparingTimer = new CountdownTimer(0f);
        attackTimer = new CountdownTimer(0f);
        attackPreparingTimer.Finished += delegate
        {
            Debug.Log("Cast Attack");
            attackPosition = playerTransform.position;
            attackTimer.Play(randomAttackTime);
        };
        attackTimer.Finished += Attack;

        Validate();
    }

    public virtual void Update(float deltaTime)
    {
        if (!isChasing && Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance)
        {
            moveSpeed = chaseSpeed;
            isChasing = true;
        }

        if (isChasing)
        {
            if (!attackTimer.IsPlaying)
            {
                if (transform.Distance(playerTransform) <= attackDistance)
                {
                    if (!attackPreparingTimer.IsPlaying)
                    {
                        Debug.LogWarning("Attack Preparing");
                        attackPreparingTimer.Play(randomAttackPreparingTime);
                    }
                }
                else if (attackPreparingTimer.IsPlaying)
                    attackPreparingTimer.Stop();

                CalculateTargetPoint();
                MoveTo(targetPosition, deltaTime);
            }
        }
        else
        {
            CalculateHangOnPosition();
            MoveTo(hangOnPosition, deltaTime);
        }

        transform.rotation = Quaternion.LookRotation(Player.CameraPosition - transform.position);
    }

    public void DrawGizmos()
    {
        if (!isChasing)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetPosition, hangOnRadius);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        if (attackTimer.IsPlaying)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(attackPosition, attackOverlapRadius);
        }
    }

    protected virtual void MoveTo(Vector3 position, float deltaTime)
    {
        if (position == transform.position)
            return;

        Vector3 direction = position - transform.position;
        Vector3 nextMove = moveSpeed * deltaTime * direction;

        if (nextMove.magnitude >= transform.position.Distance(position))
        {
            transform.position = position;
        }
        else
        {
            transform.position += nextMove;
        }
    }

    protected virtual void Attack()
    {
        Debug.LogWarning("Attack");
    }

    protected virtual void CalculateHangOnPosition()
    {
        if (!hangOnTimer.IsPlaying)
        {
            hangOnPosition = GetRandomPointAround(targetPosition, hangOnRadius);
            hangOnTimer.Play(randomHangOnTime);
        }
    }

    protected virtual void CalculateTargetPoint()
    {
        if (targetPosition.Distance(playerTransform.position) < offset)
            return;

        Vector3 randomPoint = Random.insideUnitSphere * (offset - 0.001f);
        randomPoint.y = Mathf.Abs(randomPoint.y) + bodyRadius;

        targetPosition = playerTransform.position + randomPoint;
    }

    protected Vector3 GetRandomPointAround(Vector3 position, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere;

        if (Physics.Raycast(position, randomDirection, out RaycastHit hitInfo, radius))
            return hitInfo.point + (bodyRadius * hitInfo.normal);
        else
            return position + radius * randomDirection;
    }

    private void Validate()
    {
        if (chaseDistance < offset)
            Debug.LogError("[FlyingEnemyBehaviour] Chase Distance should not be less tha Offset");
    }
}