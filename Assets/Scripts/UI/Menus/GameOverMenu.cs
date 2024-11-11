using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : BaseMenu
{
    [Header("Buttons")]
    [SerializeField] private Button reloadLevelButton;
    [SerializeField] private Button mainMenuButton;

    protected override void Start()
    {
        base.Start();

        //Player.Health.Died += OnPlayerDied;

        reloadLevelButton.onClick.AddListener(SceneController.ReloadScene);
        mainMenuButton.onClick.AddListener(SceneController.LoadMainMenu);
    }

    private void OnPlayerDied()
    {
        Show();
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.PauseGame();
    }
}