using UnityEngine;

public class DefaultAngelBehaviour
{
    public bool isChasing;
    public Vector3 targetPosition;
    public Vector3 attackPosition;
    public CountdownTimer attackTimer;

    protected float moveSpeed;
    protected float bodyRadius;

    protected Vector3 hangOnPosition;
    protected CountdownTimer hangOnTimer;

    protected CountdownTimer attackPreparingTimer;

    protected AngelEnemy owner;
    protected Transform playerTransform;

    protected AngelEnemyConfig config;

    protected Transform transform => owner.transform;
    protected float randomAttackPreparingTime => Random.Range(config.attackPreparingTime.min, config.attackPreparingTime.max);
    protected float randomAttackTime => Random.Range(config.attackTime.min, config.attackTime.max);
    protected float randomHangOnTime => Random.Range(config.hangOnTime.min, config.hangOnTime.max);

    public DefaultAngelBehaviour(AngelEnemy owner, AngelEnemyConfig config, float bodyRadius)
    {
        this.config = config;
        this.bodyRadius = bodyRadius;

        this.owner = owner;
        this.playerTransform = Player.OriginTransform;

        targetPosition = transform.position;

        SetupTimers();
        Validate();
    }

    public virtual void Update(float deltaTime)
    {
        if (!isChasing && transform.Distance(playerTransform) <= config.chaseDistance)
        {
            moveSpeed = config.chaseSpeed;
            isChasing = true;
        }

        if (isChasing)
        {
            if (!attackTimer.IsPlaying)
            {
                if (transform.Distance(playerTransform) <= config.attackDistance)
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
        Debug.Log("Attack");
        if (Physics.OverlapSphere(attackPosition, config.attackOverlapRadius, config.playerLayerMask) != null)
            Player.Health.Damage(config.damage);
    }

    protected virtual void CalculateHangOnPosition()
    {
        if (!hangOnTimer.IsPlaying)
        {
            hangOnPosition = GetRandomPointAround(targetPosition, config.hangOnRadius);
            hangOnTimer.Play(randomHangOnTime);
        }
    }

    protected virtual void CalculateTargetPoint()
    {
        if (targetPosition.Distance(playerTransform.position) < config.offset)
            return;

        Vector3 randomPoint = Random.insideUnitSphere * (config.offset - 0.001f);
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

    private void SetupTimers()
    {
        hangOnTimer = new CountdownTimer(0f);

        attackPreparingTimer = new CountdownTimer(0f);
        attackTimer = new CountdownTimer(0f);
        attackPreparingTimer.Finished += delegate
        {
            Debug.Log("Cast Attack");
            attackPosition = Player.CameraPosition;
            float attackDuration = randomAttackTime;

            Object.Instantiate(config.attackParticle, attackPosition, Quaternion.identity).
                Init(attackDuration, config.attackParticleColor).Play();

            attackTimer.Play(attackDuration);
        };
        attackTimer.Finished += Attack;
    }

    private void Validate()
    {
        if (config.chaseDistance < config.offset)
            Debug.LogError("[FlyingEnemyBehaviour] Chase Distance should not be less than Offset");
    }
}