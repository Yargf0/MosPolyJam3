using UnityEngine;

public class HealerMageEnemy : BaseEnemy
{
    [Header("Heal Settings")]
    [SerializeField] private float healValue = 1f;
    [SerializeField] private float healRadius = 10f;

    [SerializeField] private float healCooldown = 5f;
    [SerializeField] private float healCastTime = 5f;
    [SerializeField] private LayerMask playerLayerMask;
    [Space(10)]
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private HealerAttackParticle healParticlePrefab;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 healPosition;

    private CountdownTimer cooldownTimer;
    private CountdownTimer castPreparingTimer;

    private Transform playerTransform;

    private bool CanCast => !cooldownTimer.IsPlaying && !castPreparingTimer.IsPlaying;

    protected override void Start()
    {
        playerTransform = Player.OriginTransform;

        cooldownTimer = new CountdownTimer();
        castPreparingTimer = new CountdownTimer();

        castPreparingTimer.Finished += CastHeal;

        base.Start();
    }

    private void CastHeal()
    {
        float duration = (projectileSpawnPoint.position.Distance(healPosition) / moveSpeed) * Time.deltaTime;
        Instantiate(healParticlePrefab, projectileSpawnPoint.position, Quaternion.identity).
            Init(duration, healPosition).
            OnComplete(delegate
            {
                if (Physics.CheckSphere(healPosition, healRadius, playerLayerMask))
                {
                    if (isInverted) Player.Health.Damage(healValue);
                    else Player.Health.Heal(healValue);
                }
            });
    }

    private void Update()
    {
        if (CanCast && transform.Distance(playerTransform) <= healRadius)
        {
            transform.rotation = Quaternion.LookRotation(playerTransform.DirectionXZ(transform));

            healPosition = Player.CameraPosition;

            cooldownTimer.Play(healCooldown + healCastTime);
            castPreparingTimer.Play(healCastTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, healRadius);

        if (castPreparingTimer != null && castPreparingTimer.IsPlaying)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(healPosition, 1f);
        }
    }

    protected override void OnInverted() { }
}