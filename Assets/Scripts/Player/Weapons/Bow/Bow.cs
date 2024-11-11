using UnityEngine;

public class Bow : BaseWeapon
{
    [Space(5)]
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float shootForce;
    [Space(5)]
    [SerializeField] private Animator animator;

    private readonly int AnimationHash = Animator.StringToHash("shoot");

    protected override void Animate()
    {
        if (cooldownTimer.IsPlaying)
            return;

        animator.Play(AnimationHash);
        cooldownTimer.Play(cooldown);
    }

    protected override void Attack()
    {
        Arrow arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        arrow.Init(damage, shootForce * Player.LookDirection);
    }
}