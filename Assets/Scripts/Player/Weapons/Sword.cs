using DG.Tweening;
using UnityEngine;

public class Sword : PlayerModule
{
    [Header("Settings")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float attackCooldown = 2f;

    [Header("References")]
    [SerializeField] private Transform handGripTransform;

    [Header("Animation")]
    [SerializeField] TweenOptions tweenOptions = new(0.5f, Ease.InCubic);

    private Sequence sequence;

    private CountdownTimer attackCooldownTimer;

    public override void Init(PlayerInput input)
    {
        base.Init(input);

        input.OnAttack += Animate;

        attackCooldownTimer = new CountdownTimer();
    }

    private void Animate()
    {
        if (attackCooldownTimer.IsPlaying)
            return;

        sequence?.Kill();
        sequence = DOTween.Sequence().
            Append(handGripTransform.
                DOLocalRotate(new Vector3(45f, 0f, 0f), tweenOptions.Duration).
                SetEase(tweenOptions.Ease).
                OnComplete(Attack)).
            Append(handGripTransform.
                DOLocalRotate(Vector3.zero, tweenOptions.Duration).
                SetEase(tweenOptions.Ease)).
            Play();

        attackCooldownTimer.Play(attackCooldown);
    }

    private void Attack()
    {
        Collider[] enemiesColliders = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyLayerMask);

        for (int i = 0; i < enemiesColliders.Length; i++)
        {
            if (enemiesColliders[i].TryGetComponent(out BaseEnemy enemy))
            {
                enemy.HealthSystem.Damage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}