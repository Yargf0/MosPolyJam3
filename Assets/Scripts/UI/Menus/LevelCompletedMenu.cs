using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedMenu : BaseMenu, IGameFinishListener
{
    [Header("Buttons")]
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Audio")]
    [SerializeField] private AudioClip levelCompletedAudio;

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.RegisterListener(this);

        nextLevelButton.onClick.AddListener(SceneController.LoadNextScene);
        mainMenuButton.onClick.AddListener(SceneController.LoadMainMenu);
    }

    public void OnGameFinished()
    {
        Show();
        AudioManager.Instance.PlaySound(levelCompletedAudio, Random.Range(0.9f, 1.1f));
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.PauseGame();
    }
}