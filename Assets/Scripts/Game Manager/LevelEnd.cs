using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private AudioSource whiteNoiseSource;

    private void Start()
    {
        AudioManager.Instance.SoundVolume.ValueChanged += (pV, nV) => whiteNoiseSource.volume = nV;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            GameManager.Instance.FinishGame();
        }
    }
}