using UnityEngine;


public class HealthPack : InvertableBehaviour, ICollectable
{
    [Header("Health Pack Settings")]
    public float healAmount = 20f;
    public float damageAmount = 10f;

    [SerializeField] private AudioClip collectedAudio;

    public void Collect()
    {

        if (isInverted)
        {
            Player.Instance.Health.Damage(damageAmount);
        }
        else
        {
            Player.Instance.Health.Heal(healAmount);
        }

        AudioManager.Instance.PlaySoundAtPosition(collectedAudio, transform.position, Random.Range(0.9f, 1.1f));

        Destroy(gameObject);

    }
    protected void Update()
    {
        Vector3 direction = Player.Instance.CameraPosition - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    protected override void OnInverted()
    {
        // Здесь можно добавить реакцию на инверсию, если это потребуется
    }
}

