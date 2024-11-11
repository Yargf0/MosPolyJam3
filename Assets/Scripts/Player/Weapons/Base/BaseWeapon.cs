using UnityEngine;

public abstract class BaseWeapon : PlayerModule
{
    [Header("Settings")]
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected float cooldown = 2f;

    protected CountdownTimer cooldownTimer;

    public override void Init(PlayerInput input)
    {
        base.Init(input);

        input.OnAttack += Animate;

        cooldownTimer = new CountdownTimer();
    }

    protected abstract void Animate();
    protected abstract void Attack();
}