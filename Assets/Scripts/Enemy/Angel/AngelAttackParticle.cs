using UnityEngine;

public class AngelAttackParticle : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem collectParticle;
    [SerializeField] private ParticleSystem explosionParticle;

    private CountdownTimer timer;

    public AngelAttackParticle Init(float duration, ParticleSystem.MinMaxGradient color)
    {
        timer = new CountdownTimer();

        // collect particle
        var main = collectParticle.main;
        main.duration = duration;
        main.startColor = color;

        // explosion particle
        main = explosionParticle.main;
        main.startColor = color;

        return this;
    }

    public void Play()
    {
        if (timer.IsPlaying)
            timer.Stop();

        timer.OnFinished(delegate
        {
            timer.OnFinished(delegate
            {
                Destroy(gameObject);
            });

            timer.Play(explosionParticle.main.duration);
            explosionParticle.Play();
        });

        timer.Play(collectParticle.main.duration);
        collectParticle.Play();
    }
}