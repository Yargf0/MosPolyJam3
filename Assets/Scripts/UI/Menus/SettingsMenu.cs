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
    [SerializeField] private Slider fovSlider;
    [SerializeField] private Slider mouseSlider;
    [SerializeField] private AimCursor mouseAim;

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
        if (Player.Instance)
        {
            fovSlider.onValueChanged.AddListener((value) => Player.Instance.Cam.maxFOV = value);

            sensetivitySlider.onValueChanged.AddListener((value) => Player.Instance.Cam.sensetivityMultiplayer = value);
            mouseSlider.onValueChanged.AddListener((value) => mouseAim.SetCursor(value));
        }
    }

    private void SetupView()
    {
        //muteMusicToggle.IsOn.Value = !AudioManager.Instance.MuteMusic.Value;
        musicSlider.SetValueWithoutNotify(AudioManager.Instance.MusicVolume.Value);

        //muteSoundToggle.IsOn.Value = !AudioManager.Instance.MuteSound.Value;
        soundSlider.SetValueWithoutNotify(AudioManager.Instance.SoundVolume.Value);

        if (Player.Instance)
        {
            fovSlider.SetValueWithoutNotify(Player.Instance.Cam.maxFOV);

            sensetivitySlider.SetValueWithoutNotify(Player.Instance.Cam.sensetivityMultiplayer);
            mouseSlider.SetValueWithoutNotify(mouseAim.cursor);
        }

    }
}