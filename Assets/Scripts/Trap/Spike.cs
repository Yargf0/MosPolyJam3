using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spike : InvertableBehaviour
{
    public float DamageInterval = 3.0f;
    public float Damage = 20f;
    private Coroutine damageCorutin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isInverted)
                Player.Health.Heal(Damage);
            else
                Player.Health.Damage(Damage);
            damageCorutin = StartCoroutine(RestartSpike());
        }
    }

    private IEnumerator RestartSpike()
    {
        GetComponent<Collider>().enabled = false;    
        yield return new WaitForSeconds(DamageInterval);
        GetComponent<Collider>().enabled = true;
    }

    protected override void OnInverted()
    {
        // Здесь можно добавить реакцию на инверсию, если это потребуется
    }
}

