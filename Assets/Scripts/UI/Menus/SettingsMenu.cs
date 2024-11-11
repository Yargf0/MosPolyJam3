using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    [Header("Music Controls")]
    //[SerializeField] private TweenToggle muteMusicToggle;
    [SerializeField] private Slider musicSlider;
    //[Space(10)]
    //[SerializeField] private TweenToggle muteSoundToggle;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider sensetivitySlider;

    protected override void Start()
    {
        base.Start();

        SetupView();
        SetupControls();
    }

    private void SetupControls()
    {
        //muteMusicToggle.IsOn.ValueChanged += (prevValue, newValue) => AudioManager.Instance.MuteMusic.Value = !newValue;
        musicSlider.onValueChanged.AddListener((value) => AudioManager.Instance.MusicVolume.Value = value);

        //muteSoundToggle.IsOn.ValueChanged += (prevValue, newValue) => AudioManager.Instance.MuteSound.Value = !newValue;
        soundSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SoundVolume.Value = value);
    }

    private void SetupView()
    {
        //muteMusicToggle.IsOn.Value = !AudioManager.Instance.MuteMusic.Value;
        musicSlider.SetValueWithoutNotify(AudioManager.Instance.MusicVolume.Value);

        //muteSoundToggle.IsOn.Value = !AudioManager.Instance.MuteSound.Value;
        soundSlider.SetValueWithoutNotify(AudioManager.Instance.SoundVolume.Value);
    }
}