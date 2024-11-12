using UnityEngine;

public static class SaveSystem
{
    public static int GetInt(string name, int defaultValue = 0) => PlayerPrefs.GetInt(name, defaultValue);
    public static float GetFloat(string name, float defaultValue = 0f) => PlayerPrefs.GetFloat(name, defaultValue);
    public static string GetString(string name, string defaultValue) => PlayerPrefs.GetString(name, defaultValue);

    public static void SetInt(string name, int value) => PlayerPrefs.SetInt(name, value);
    public static void SetFloat(string name, float value) => PlayerPrefs.SetFloat(name, value);
    public static void SetString(string name, string value) => PlayerPrefs.SetString(name, value);
}