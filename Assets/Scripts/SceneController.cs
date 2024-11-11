using DG.Tweening;
using UnityEngine.SceneManagement;

public static class SceneController
{
    private readonly static int scenesCount = SceneManager.sceneCountInBuildSettings;

    private static int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

    public static void LoadMainMenu()
    {
        LoadScene(1);
    }

    public static void ReloadScene()
    {
        LoadScene(CurrentSceneIndex);
    }

    public static void LoadLevel(int levelIndex)
    {
        LoadScene(levelIndex + 1);
    }

    public static void LoadNextScene()
    {
        if (CurrentSceneIndex < scenesCount - 1)
            LoadScene(CurrentSceneIndex + 1);
        else
            LoadMainMenu();
    }

    public static void LoadScene(int buildIndex)
    {
        DOTween.KillAll();
        DOTween.Clear();
        SceneManager.LoadScene(buildIndex);
    }
}