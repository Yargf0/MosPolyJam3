using System.Collections.Generic;
using UnityEngine;

public class LevelsMenu : BaseMenu
{
    [Header("Level Buttons")]
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private LevelButton buttonPrefab;
    [SerializeField] private int levelsCount;

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

        buttons = new List<LevelButton>(levelsCount);

        for (int i = 1; i <= levelsCount; i++)
        {
            LevelButton button = Instantiate(buttonPrefab, buttonsContainer);
            button.SetData(i, 3);
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