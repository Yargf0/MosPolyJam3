using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InvertableBehaviour), true)]
public class InvertableBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        InvertableBehaviour invertableBehaviour = (InvertableBehaviour)target;
        SerializedProperty isInverted = new SerializedObject(invertableBehaviour).FindProperty("isInverted");
        if (GUILayout.Button("Invert"))
        {
            invertableBehaviour.SetInvertable(!isInverted.boolValue);
        }
    }
}