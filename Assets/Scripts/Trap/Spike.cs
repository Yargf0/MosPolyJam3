using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float DamageInterval = 3.0f;
    public float Damage = 20f;
    private bool isPlayerOnSpikes = false;
    private Coroutine damageCorutin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            health.Damage(Damage);
            isPlayerOnSpikes = true;
            damageCorutin = StartCoroutine(ApplyDamageOverTime(health));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ������������� ����, ���� ����� �������� ����
        if (other.CompareTag("Player"))
        {
            isPlayerOnSpikes = false;
            StopCoroutine(damageCorutin);
        }
    }

    private IEnumerator ApplyDamageOverTime(HealthSystem health)
    {
        // ���� ����� ��������� �� �����, ������� ���� � �������� ����������
        while (isPlayerOnSpikes)
        {
            yield return new WaitForSeconds(DamageInterval);
            if (isPlayerOnSpikes) // ��������� ����� �� ������, ���� ����� ����� ����
            {
                health.Damage(Damage);
            }
        }
    }
}

