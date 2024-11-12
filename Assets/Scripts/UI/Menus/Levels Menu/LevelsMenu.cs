using System.Collections.Generic;
using UnityEngine;

public class LevelsMenu : BaseMenu
{
    [Header("Level Buttons")]
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private LevelButton buttonPrefab;

    private List<LevelButton> buttons;

    protected override void Start()
    {
        base.Start();

        InstantiateButtons();
    }

    private void InstantiateButtons()
    {
        if (buttons != null)
            DestroyButtons();

        List<LevelData> levelDatas = LevelsManager.Instance.LevelDatas;
        buttons = new List<LevelButton>(levelDatas.Count);

        for (int i = 0; i < levelDatas.Count; i++)
        {
            LevelButton button = Instantiate(buttonPrefab, buttonsContainer);
            button.SetData(levelDatas[i]);
            buttons.Add(button);
        }
    }

    private void DestroyButtons()
    {
        foreach (LevelButton button in buttons)
            Destroy(button.gameObject);

        buttons.Clear();
    }
}