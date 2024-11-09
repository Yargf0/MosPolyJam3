using DG.Tweening;
using UnityEngine.SceneManagement;

public static class SceneController
{
    private static int currentSceneIndex;
    private static int scenesCount = SceneManager.sceneCountInBuildSettings;

    public static void LoadMainMenu()
    {
        LoadScene(1);
    }

    public static void ReloadScene()
    {
        LoadScene(currentSceneIndex);
    }

    public static void LoadLevel(int levelIndex)
    {
        LoadScene(levelIndex - 2);
    }

    public static void LoadNextScene()
    {
        if (currentSceneIndex < scenesCount - 1)
            LoadScene(currentSceneIndex + 1);
        else
            LoadMainMenu();
    }

    public static void LoadScene(int buildIndex)
    {
        DOTween.Clear();

        currentSceneIndex = buildIndex;
        SceneManager.LoadScene(buildIndex);
    }
}