using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundAudioSource;

    [Header("Music")]
    [SerializeField] private AudioClip musicClip;

    public Observer<bool> MuteMusic { get; private set; } = new();
    public Observer<float> MusicVolume { get; private set; } = new(1f);

    public Observer<bool> MuteSound { get; private set; } = new();
    public Observer<float> SoundVolume { get; private set; } = new(1f);

    private void Start()
    {
        MuteMusic.ValueChanged += (pV, nV) => musicAudioSource.mute = nV;
        MusicVolume.ValueChanged += (pV, nV) => musicAudioSource.volume = nV * 0.5f;

        MuteSound.ValueChanged += (pV, nV) => soundAudioSource.mute = nV;
        SoundVolume.ValueChanged += (pV, nV) => soundAudioSource.volume = nV * 0.5f;

        Load();

        musicAudioSource.loop = true;
        PlayMusic(musicClip);
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Load()
    {
        MuteMusic.Value = SaveSystem.GetInt(nameof(MuteMusic)) != 0;
        MusicVolume.Value = SaveSystem.GetFloat(nameof(MusicVolume), 0.2f);

        MuteSound.Value = SaveSystem.GetInt(nameof(MuteSound)) != 0;
        SoundVolume.Value = SaveSystem.GetFloat(nameof(SoundVolume), 0.2f);
    }

    private void Save()
    {
        SaveSystem.SetInt(nameof(MuteMusic), MuteMusic.Value ? 1 : 0);
        SaveSystem.SetFloat(nameof(MusicVolume), MusicVolume.Value);

        SaveSystem.SetInt(nameof(MuteSound), MuteSound.Value ? 1 : 0);
        SaveSystem.SetFloat(nameof(SoundVolume), SoundVolume.Value);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void PlaySound(AudioClip clip, float pitch = 1f)
    {
        soundAudioSource.pitch = pitch;
        soundAudioSource.PlayOneShot(clip);
    }

    public void PlaySound(AudioSource source, AudioClip clip, float pitch = 1f)
    {
        source.mute = MuteSound.Value;
        source.volume = SoundVolume.Value;

        source.pitch = pitch;
        source.PlayOneShot(clip);
    }
}