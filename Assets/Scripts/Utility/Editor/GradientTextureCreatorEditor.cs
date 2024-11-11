using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GradientTextureCreator))]
public class GradientTextureCreatorEditor : Editor
{
    private GradientTextureCreator gradientCreator;
    private static string textureName = "New Gradient";

    private void OnEnable()
    {
        gradientCreator = (GradientTextureCreator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        textureName = EditorGUILayout.TextField(textureName);

        if (GUILayout.Button("Generate"))
        {
            SaveTexture(gradientCreator.GenerateTexture(), $"Assets/{textureName}.png");
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SaveTexture(Texture2D texture, string relativePath)
    {
        byte[] bytes = texture.EncodeToPNG();
        string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
        File.WriteAllBytes(absolutePath, bytes);

        AssetDatabase.Refresh();
        Object asset = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
        EditorGUIUtility.PingObject(asset);
    }
}