using System.Collections;
using UnityEngine;

public class Barrel : InvertableBehaviour, IDamagable
{
    public GameObject ObjectToActivate;
    public ParticleSystem particleSystem;
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
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); 
        particleSystem.Play(true); 
    }

    private void StartInverseParticleCoroutine()
    {
        StopAllCoroutines(); 
        StartCoroutine(InverseParticle());
    }

    
    IEnumerator InverseParticle()
    {
        // ��������� ������������� �������, ����� ����� ���� ������� AutoRandomSeed
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // ������� ����, ����� ���������, ��� ��� ������� �������
        yield return new WaitForSeconds(0.1f);

        // ��������� ��������
        bool useAutoRandomSeed = particleSystem.useAutoRandomSeed;
        particleSystem.useAutoRandomSeed = false;

        // �������������� ���������
        currentSimulationTime = startTime;

        // ��������� ��������� � �������� �����������
        while (true)
        {
            particleSystem.Play(false);
            float deltaTime = particleSystem.main.useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            currentSimulationTime -= deltaTime * particleSystem.main.simulationSpeed * simulationSpeedScale;
            particleSystem.Simulate(currentSimulationTime, true, true, true);

            if (currentSimulationTime <= 0)
            {
                currentSimulationTime = T;
            }

            yield return null;
        }

        // ��������������� ��������� AutoRandomSeed, ���� ����������
        particleSystem.useAutoRandomSeed = useAutoRandomSeed;
    }
}


