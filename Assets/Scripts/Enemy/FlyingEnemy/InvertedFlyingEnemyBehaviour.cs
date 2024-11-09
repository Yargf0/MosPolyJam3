using UnityEngine;

public class InvertedFlyingEnemyBehaviour : DefaultFlyingEnemyBehaviour
{
    public InvertedFlyingEnemyBehaviour(FlyingEnemy owner, float chaseSpeed, LayerMask excludePlayerLayerMask) :
        base(owner, chaseSpeed, excludePlayerLayerMask) { }
    
    protected override void CalculateTargetPoint()
    {
        Vector3 playerLookDirection = Player.LookDirection;
        Vector3 playerCameraPosition = Player.CameraPosition;

        if (Vector3.Angle(playerLookDirection, (targetPosition - playerCameraPosition)).InRange(-20f, 20f) &&
            Vector3.Distance(playerCameraPosition, targetPosition) <= offset)
            return;

        if (Physics.Raycast(playerCameraPosition, playerLookDirection, out RaycastHit hitInfo, offset, excludeLayerMask))
            targetPosition = hitInfo.point + (bodyRadius * hitInfo.normal);
        else
            targetPosition = playerCameraPosition + playerLookDirection * (offset - 0.001f);
    }
}