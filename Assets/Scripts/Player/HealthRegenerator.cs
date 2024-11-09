using System.Collections;
using UnityEngine;

public class HealthRegenerator : InvertableBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private float healthChangeAmount = 1f; // Количество изменения здоровья каждую секунду
    [SerializeField] private bool isPaused = false;

    private Coroutine regenerationCoroutine;

    // Запускает регенерацию или уменьшение здоровья на определенное время
    public void StartHealthChange(float duration)
    {
        if (regenerationCoroutine != null)
            StopCoroutine(regenerationCoroutine);

        regenerationCoroutine = StartCoroutine(ChangeHealthOverTime(duration));
    }

    // Функция для паузы
    public void PauseRegeneration()
    {
        isPaused = true;
    }

    // Функция для продолжения
    public void ResumeRegeneration()
    {
        isPaused = false;
    }

    private IEnumerator ChangeHealthOverTime(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (!isPaused && healthSystem.IsAlive)
            {
                if (isInverted)
                    healthSystem.Damage(healthChangeAmount);
                else
                    healthSystem.Heal(healthChangeAmount);
            }

            elapsedTime += 1f;
            yield return new WaitForSeconds(1f);
        }

        regenerationCoroutine = null;
    }

    protected override void OnInverted()
    {
        // Здесь можно добавить реакцию на инверсию, если это потребуется
    }
}

