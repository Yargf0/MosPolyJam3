using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedMenu : BaseMenu, IGameFinishListener
{
    [Header("Buttons")]
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;

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
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.PauseGame();
    }
}