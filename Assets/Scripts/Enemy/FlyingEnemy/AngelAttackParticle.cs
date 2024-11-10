using UnityEngine;

public class AngelAttackParticle : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem collectParticle;
    [SerializeField] private ParticleSystem explosionParticle;

    [Header("Color")]
    [SerializeField] private Color firstColor = Color.black;
    [SerializeField] private Color secondColor = Color.white;

    private CountdownTimer timer;

    private void Awake()
    {
        Init();
        Play();
    }

    private void Init()
    {
        ParticleSystem.MinMaxGradient minMaxGradient = new(secondColor, firstColor);
        Debug.Log(minMaxGradient);
        // collect particle
        var main = collectParticle.main;
        main.startColor = minMaxGradient;

        // explosion particle
        main = explosionParticle.main;
        main.startColor = minMaxGradient;
    }

    private void Play()
    {
        timer ??= new CountdownTimer();
        timer.OnFinished(delegate {
            timer.OnFinished(delegate {
                Play();
                //Destroy(gameObject);
            });

            timer.Play(explosionParticle.main.duration);
            explosionParticle.Play();
        });

        timer.Play(collectParticle.main.duration);
        collectParticle.Play();
    }
}