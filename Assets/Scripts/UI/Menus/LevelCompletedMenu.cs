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

        nextLevelButton.onClick.AddListener(SceneController.LoadNextScene);
        mainMenuButton.onClick.AddListener(SceneController.LoadMainMenu);
    }

    public void OnGameFinished()
    {
        Show();
        GameManager.Instance.PauseGame();
    }
}