using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    [Space(10)]
    [SerializeField] private Button settingsOpenButton;
    [SerializeField] private Button settingsCloseButton;
    [Space(10)]
    [SerializeField] private Button mainMenuButton;

    [Header("Input")]
    [SerializeField] private InputAction pauseAction;

    private bool isPaused = false;

    protected override void Start()
    {
        if (disableOnStart)
            gameObject.SetActive(false);

        pauseAction.Enable();
        pauseAction.performed += OnPauseActionPerformed;

        closeButton.onClick.AddListener(Resume);

        settingsOpenButton.onClick.AddListener(Hide);
        settingsCloseButton.onClick.AddListener(ShowWithoutAnimation);

        mainMenuButton.onClick.AddListener(SceneController.LoadMainMenu);
    }

    private void OnDestroy()
    {
        pauseAction.performed -= OnPauseActionPerformed;
    }

    public void ShowWithoutAnimation()
    {
        gameObject.SetActive(true);
    }

    private void OnPauseActionPerformed(InputAction.CallbackContext context)
    {
        if (!isPaused)
            Pause();
    }

    private void Pause()
    {
        isPaused = true;
        GameManager.Instance.PauseGame();
        Cursor.lockState = CursorLockMode.None;
        Show();
    }

    private void Resume()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.ResumeGame();
        Hide();
    }
}