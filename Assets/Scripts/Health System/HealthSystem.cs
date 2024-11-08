using System;
using UnityEngine;

public class HealthSystem : InvertableBehaviour, IDamagable
{
    public event Action Died;
    public event Action<float> HealthChanged;

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

        Health = Mathf.Max(Health - value, 0f);
        HealthChanged?.Invoke(Health);

        if (Health <= 0f)
            OnDied();
    }

    public void Heal(float value)
    {
        if (!IsAlive || value <= 0f)
            return;

        Health = Mathf.Min(Health + value, MaxHealth);
        HealthChanged?.Invoke(Health);

        if (isInverted && Health <= 0f)
            OnDied();
    }

    protected override void OnInverted()
    {
        if (isInverted)
        {
            Health = MaxHealth - Health;
        }
        else
        {
            Health = MaxHealth - Health;
        }

        HealthChanged?.Invoke(Health);
    }

    private void OnDied()
    {
        IsAlive = false;
        Died?.Invoke();
    }
}