using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : Singleton<LevelsManager>
{
    private readonly int levelsCount = 2;
    public List<LevelData> LevelDatas { get; private set; }

    protected override void Init()
    {
        initialized = true;

        DontDestroyOnLoad(this);

        LoadData();
    }

    public void SetStars(int levelSceneIndex, int starsCount = 1)
    {
        LevelData ld = GetLevelData(levelSceneIndex);
        if (ld == null)
            return;

        ld.starsCollected += starsCount;
    }

    public LevelData GetLevelData(int levelSceneIndex)
    {
        foreach (var levelData in LevelDatas)
            if (levelSceneIndex == levelData.sceneIndex)
                return levelData;

        return null;
    }

    private void LoadData()
    {
        LevelDatas = new List<LevelData>(levelsCount);
        for (int i = 0; i < levelsCount; i++)
        {
            LevelDatas.Add(new LevelData(i + 1, SaveSystem.GetInt($"{i}_sc")));
        }
    }

    private void SaveData()
    {
        for (int i = 0; i < LevelDatas.Count; i++)
        {
            SaveSystem.SetInt($"{i}_sc", LevelDatas[i].starsCollected);
        }
    }

    private void OnDestroy()
    {
        SaveData();
    }
}