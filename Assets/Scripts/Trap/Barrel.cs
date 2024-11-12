using System.Collections;
using UnityEngine;

public class Barrel : InvertableBehaviour3D, IDamagable
{
    public GameObject ObjectToActivate;
    public ParticleSystem explosionParticleSystem;
    public float startTime = 2.0f;
    public float simulationSpeedScale = 1.0f;
    public float T;
    private float currentSimulationTime;

    protected override void Start()
    {
        base.Start();
        if (isInverted)
            ObjectToActivate.SetActive(false);
        else
            ObjectToActivate.SetActive(true);
    }

    public void Damage(float damage)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (isInverted)         
            ObjectToActivate.SetActive(true);
        else
            ObjectToActivate.SetActive(false);

        if (isInverted)
            StartInverseParticleCoroutine();
        else
            PlayParticlesNormally();
    }

    protected override void OnInverted()
    {
    }

    private void PlayParticlesNormally()
    {
        StopAllCoroutines();
        explosionParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        explosionParticleSystem.Play(true);

        // Запускаем корутину, чтобы дождаться завершения анимации и уничтожить объект
        StartCoroutine(DestroyAfterNormalAnimation());
    }

    private void StartInverseParticleCoroutine()
    {
        StopAllCoroutines();
        StartCoroutine(InverseParticle());
    }

    private IEnumerator DestroyAfterNormalAnimation()
    {
        // Ожидаем завершения анимации
        yield return new WaitForSeconds(explosionParticleSystem.main.duration);

        // Уничтожаем объект
        Destroy(gameObject);
    }

    IEnumerator InverseParticle()
    {
        // Полностью останавливаем систему, чтобы можно было сменить AutoRandomSeed
        explosionParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // Немного ждем, чтобы убедиться, что все частицы исчезли
        yield return new WaitForSeconds(0.1f);

        // Отключаем автосемя
        bool useAutoRandomSeed = explosionParticleSystem.useAutoRandomSeed;
        explosionParticleSystem.useAutoRandomSeed = false;

        // Инициализируем симуляцию
        currentSimulationTime = startTime;

        // Запускаем симуляцию в обратном направлении
        while (currentSimulationTime > 0)
        {
            explosionParticleSystem.Play(false);
            float deltaTime = explosionParticleSystem.main.useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            currentSimulationTime -= deltaTime * explosionParticleSystem.main.simulationSpeed * simulationSpeedScale;
            explosionParticleSystem.Simulate(currentSimulationTime, true, true, true);

            // Если анимация завершена
            if (currentSimulationTime <= 0)
            {
                break;
            }

            yield return null;
        }

        // Восстанавливаем состояние AutoRandomSeed, если необходимо
        //particleSystem.useAutoRandomSeed = useAutoRandomSeed;

        // Уничтожаем объект после завершения обратной анимации
        Destroy(gameObject);
    }
}



