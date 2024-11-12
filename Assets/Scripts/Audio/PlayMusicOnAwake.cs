using UnityEngine;

public class PlayMusicOnAwake : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(music);
        Destroy(gameObject);
    }
}