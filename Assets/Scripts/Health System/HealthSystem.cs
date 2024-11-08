using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    public event Action Died;
    public event Action<float> Damaged;
    public event Action<float> Healed;

    public float MaxHealth { get; private set; }
    public float Health { get; private set; }
    public bool IsAlive { get; private set; }

    public void Init(float maxHealth)
    {
        if (maxHealth <= 0f)
        {
            Debug.LogError($"[{nameof(HealthSystem)}] Invalid setted {nameof(maxHealth)} ({maxHealth}). Must be positive");
            return;
        }

        MaxHealth = maxHealth;
        Health = 0f;
        IsAlive = true;
    }

    public void Damage(float value)
    {
        if (!IsAlive || value <= 0f)
            return;

        Health = Mathf.Min(Health + value, MaxHealth);
        Damaged?.Invoke(Health);

        if (Health >= MaxHealth)
            OnDied();
    }

    public void Heal(float value)
    {
        if (!IsAlive || value <= 0f)
            return;

        Health = Mathf.Max(Health - value, 0f);
        Healed?.Invoke(Health);
    }

    private void OnDied()
    {
        IsAlive = false;
        Died?.Invoke();
    }
}