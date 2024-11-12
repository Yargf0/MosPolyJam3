using UnityEngine;

public abstract class BaseWeapon : PlayerModule
{
    [Header("Settings")]
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected float cooldown = 2f;

    [Header("Audio")]
    [SerializeField] protected AudioClip attackAudio;

    protected CountdownTimer cooldownTimer;

    public override void Init(PlayerInput input)
    {
        base.Init(input);


        cooldownTimer = new CountdownTimer();
    }

    public virtual void Enable()
    {
        input.OnAttack += Animate;
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        input.OnAttack -= Animate;
        gameObject.SetActive(false);
    }

    protected abstract void Animate();
    protected abstract void Attack();
}