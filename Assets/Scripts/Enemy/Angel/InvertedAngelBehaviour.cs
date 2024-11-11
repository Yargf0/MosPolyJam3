using UnityEngine;

public class InvertedAngelBehaviour : DefaultAngelBehaviour
{
    public InvertedAngelBehaviour(AngelEnemy owner, AngelEnemyConfig config, float bodyRadius) :
        base(owner, config, bodyRadius)
    { }

    protected override void Attack()
    {
        Debug.Log("Attack");
        if (Physics.OverlapSphere(attackPosition, config.attackOverlapRadius, config.playerLayerMask) != null)
            Player.Instance.Health.Heal(config.damage);
    }

    protected override void CalculateTargetPoint()
    {
        Vector3 playerLookDirection = Player.Instance.LookDirection;
        Vector3 playerCameraPosition = Player.Instance.CameraPosition;

        if (playerLookDirection.Angle(targetPosition - playerCameraPosition).InRange(config.chaseAngle) &&
            playerCameraPosition.Distance(targetPosition) <= config.offset)
            return;

        if (Physics.Raycast(playerCameraPosition, playerLookDirection, out RaycastHit hitInfo, config.offset, config.excludeInMovementLayerMask))
            targetPosition = hitInfo.point + (bodyRadius * hitInfo.normal);
        else
            targetPosition = playerCameraPosition + playerLookDirection * (config.offset - 0.001f);
    }
}