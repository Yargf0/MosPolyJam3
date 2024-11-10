using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Music")]
    [SerializeField] private AudioClip musicClip;

    private AudioSource musicAudioSource;
    private AudioSource soundAudioSource;

    public Observer<bool> MuteMusic { get; private set; } = new();
    public Observer<float> MusicVolume { get; private set; } = new(1f);

    public Observer<bool> MuteSound { get; private set; } = new();
    public Observer<float> SoundVolume { get; private set; } = new(1f);

    protected override void Init()
    {
        CreateAudioSources();

        MuteMusic.ValueChanged += (pV, nV) => musicAudioSource.mute = nV;
        MusicVolume.ValueChanged += (pV, nV) => musicAudioSource.volume = nV * 0.5f;
        
        MuteSound.ValueChanged += (pV, nV) => soundAudioSource.mute = nV;
        SoundVolume.ValueChanged += (pV, nV) => soundAudioSource.volume = nV * 0.5f;

        LoadSettings();
        PlayMusic(musicClip);
    }

    private void OnDestroy()
    {
        SaveSettings();
    }

    private void CreateAudioSources()
    {
        musicAudioSource = new GameObject("Music Audio Source").AddComponent<AudioSource>();
        musicAudioSource.transform.parent = transform;
        musicAudioSource.loop = true;
        musicAudioSource.playOnAwake = false;

        soundAudioSource = new GameObject("Sound Audio Source").AddComponent<AudioSource>();
        soundAudioSource.transform.parent = transform;
        soundAudioSource.playOnAwake = false;
    }

    private void LoadSettings()
    {
        MuteMusic.Value = SaveSystem.GetInt(nameof(MuteMusic)) != 0;
        MusicVolume.Value = SaveSystem.GetFloat(nameof(MusicVolume), 0.2f);

        MuteSound.Value = SaveSystem.GetInt(nameof(MuteSound)) != 0;
        SoundVolume.Value = SaveSystem.GetFloat(nameof(SoundVolume), 0.2f);
    }

    private void SaveSettings()
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