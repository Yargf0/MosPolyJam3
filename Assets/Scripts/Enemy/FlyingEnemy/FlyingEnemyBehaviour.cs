using UnityEngine;

public abstract class FlyingEnemyBehaviour
{
    public abstract void Update(float deltaTime);
}

public class FrontFlyingEnemyBehaviour : FlyingEnemyBehaviour
{
    private float moveSpeed;
    private float offset = 3f;
    private Vector3 targetPosition;
    private Vector3 direction;

    private float radius;

    private LayerMask excludePlayerLayerMask;

    private FlyingEnemy owner;
    private Transform playerTransform;

    private Transform transform => owner.transform;

    public FrontFlyingEnemyBehaviour(FlyingEnemy owner, float moveSpeed, LayerMask excludePlayerLayerMask)
    {
        this.moveSpeed = moveSpeed;
        this.radius = 0.5f; // TODO: get from owner
        this.excludePlayerLayerMask = excludePlayerLayerMask;

        this.owner = owner;
        this.playerTransform = Player.OriginTransform;
    }

    public override void Update(float deltaTime)
    {
        CalculateTargetPoint();
        MoveToTarget(deltaTime);
    }

    private void MoveToTarget(float deltaTime)
    {
        Vector3 nextMove = moveSpeed * deltaTime * direction;

        if (nextMove.magnitude >= Vector3.Distance(transform.position, targetPosition))
        {
            transform.position = targetPosition;
        }
        else
        {
            transform.position += nextMove;
        }
    }

    private void CalculateTargetPoint()
    {
        Vector3 playerLookDirection = Player.LookDirection;
        Vector3 playerCameraPosition = Player.CameraPosition;

        float angle = Vector3.Angle(playerLookDirection, (transform.position - playerCameraPosition));

        if (Vector3.Distance(playerCameraPosition, transform.position) <= offset && angle > -10f && angle < 10f)
            return;

        if (Physics.Raycast(playerCameraPosition, playerLookDirection, out RaycastHit hitInfo, offset, excludePlayerLayerMask))
            targetPosition = hitInfo.point + (radius * -playerLookDirection);
        else
            targetPosition = playerCameraPosition + playerLookDirection * offset;

        direction = (targetPosition - transform.position).normalized;
    }
}