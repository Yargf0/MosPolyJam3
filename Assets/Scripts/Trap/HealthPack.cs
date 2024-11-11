using UnityEngine;


public class HealthPack : InvertableBehaviour, ICollectable
{
    [Header("Health Pack Settings")]
    public float healAmount = 20f;
    public float damageAmount = 10f;

    public void Collect()
    {

        if (isInverted)
        {
            Player.Health.Damage(damageAmount);
        }
        else
        {
            Player.Health.Heal(healAmount);
        }

        Destroy(gameObject);

    }
    protected void Update()
    {
        Vector3 direction = Player.CameraPosition - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    protected override void OnInverted()
    {
        // Здесь можно добавить реакцию на инверсию, если это потребуется
    }
}

