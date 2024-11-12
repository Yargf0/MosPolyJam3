using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image[] starFills;

    private Button button;

    public void SetData(LevelData levelData)
    {
        label.text = levelData.sceneIndex.ToString();

        button ??= GetComponent<Button>();
        button.onClick.AddListener(delegate
        {
            SceneController.LoadScene(levelData.sceneIndex);
        });

        for (int i = 0; i < starFills.Length; i++)
        {
            if (i < levelData.starsCollected)
                starFills[i].enabled = true;
            else
                starFills[i].enabled = false;
        }
    }
}