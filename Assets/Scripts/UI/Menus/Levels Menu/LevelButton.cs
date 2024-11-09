using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image[] starFills;

    private Button button;

    public void SetData(int levelNumber, int starsCount)
    {
        label.text = levelNumber.ToString();

        button ??= GetComponent<Button>();
        button.onClick.AddListener(delegate
        {
            SceneController.LoadLevel(levelNumber);
        });

        for (int i = 0; i < starFills.Length; i++)
        {
            if (i < starsCount)
                starFills[i].enabled = true;
            else
                starFills[i].enabled = false;
        }
    }
}