using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject systems;

    private void Awake()
    {
        DontDestroyOnLoad(systems);

        SceneController.LoadMainMenu();
    }
}