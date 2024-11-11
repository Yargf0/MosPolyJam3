using DG.Tweening;
using UnityEngine;

public class Sword : BaseWeapon
{
    [Space(5)]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;

    [Header("References")]
    [SerializeField] private Transform handGripTransform;

    [Header("Animation")]
    [SerializeField] TweenOptions tweenOptions = new(0.5f, Ease.InCubic);

    private Sequence sequence;

    protected override void Animate()
    {
        if (cooldownTimer.IsPlaying)
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

        cooldownTimer.Play(cooldown);
    }

    protected override void Attack()
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