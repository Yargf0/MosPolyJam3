using UnityEngine;

public class HealerMageEnemy : BaseEnemy
{
    [SerializeField] private float healRadius = 10f;

    [SerializeField] private float healCooldown = 5f;
    [SerializeField] private float healCastTime = 5f;

    private Vector3 healPosition;

    private CountdownTimer cooldownTimer;
    private CountdownTimer castPreparingTimer;

    private Transform playerTransform;

    private bool CanCast => !cooldownTimer.IsPlaying && !castPreparingTimer.IsPlaying;

    private void Start()
    {
        playerTransform = Player.OriginTransform;
     
        cooldownTimer = new CountdownTimer();
        castPreparingTimer = new CountdownTimer();

        castPreparingTimer.Finished += CastHeal;
    }

    private void CastHeal()
    {
        Debug.Log("Heal");
    }

    private void Update()
    {
        if (CanCast && transform.Distance(playerTransform) <= healRadius)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);

            healPosition = playerTransform.position;

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
}