using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public abstract class BaseEnemy2D : InvertableBehaviour2D
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip dieAudio;

    public HealthSystem HealthSystem { get; protected set; }

    protected virtual void Awake()
    {
        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.Init(maxHealth);

        HealthSystem.Died += OnDied;
    }

    protected virtual void OnDied()
    {
        AudioManager.Instance.PlaySoundAtPosition(dieAudio, transform.position, Random.Range(0.9f, 1.1f));
        Player.AddStar();
        Destroy(gameObject);
    }
}