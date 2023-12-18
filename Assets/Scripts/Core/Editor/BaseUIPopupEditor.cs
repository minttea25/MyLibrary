using Core;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BaseUIPopup), true)]
public class BaseUIPopupEditor : BaseUIEditor
{
    SerializedProperty content;

    private void OnEnable()
    {
        context = serializedObject.FindProperty(Const.UIContextFieldName);
        content = serializedObject.FindProperty(Const.ContentObjectFieldName);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (content != null)
        {
            EditorGUILayout.PropertyField(content, true);
            if (GUILayout.Button("Assign Content Object"))
            {
                (target as BaseUIPopup).FindContentObject();
                EditorUtility.SetDirty(target);
            }
        }
        else
        {
            Debug.LogWarning($"Can not find: {Const.ContentObjectFieldName}");
        }

        if (context != null)
        {
            EditorGUILayout.PropertyField(context, true);

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Auto - Find&Assign UI Object"))
            {
                (target as BaseUI).AutoAssignUIs();
                EditorUtility.SetDirty(target);
            }
        }
        else
        {
            Debug.LogWarning($"Can not find: {Const.UIContextFieldName}");
        }

        serializedObject.ApplyModifiedProperties();
    }
}