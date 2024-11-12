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
        AudioManager.Instance.PlaySound(attackAudio, Random.Range(0.9f, 1.1f));
        Arrow arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.LookRotation(Player.Instance.LookDirection));
        arrow.Init(damage, shootForce * Player.Instance.LookDirection);
    }
}