using Core;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseUI), true)]
public class BaseUIEditor : Editor
{
    //bool isChanged;
    protected SerializedProperty context;
    protected SerializedProperty refInfoDrawer;

    protected BaseUI ui = null;

    private void OnEnable()
    {
        ui = target as BaseUI;


        //isChanged = true;
        refInfoDrawer = serializedObject.FindProperty(Const.DBInfoFieldName);
        if (refInfoDrawer == null)
        {
            Debug.LogError($"Can not find DBInfo property in BaseUI. [name = {Const.DBInfoFieldName}]");
        }

        context = serializedObject.FindProperty(Const.UIContextFieldName);
        context ??= serializedObject.FindProperty(Const.UIContextFieldName2);
        if (context == null)
        {
            Debug.LogWarning($"Can not find UIContext property in BaseUI. [name = {Const.UIContextFieldName} | {Const.UIContextFieldName2}");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(refInfoDrawer);
        EditorGUILayout.Separator();


        if (context != null)
        {
            // section 1 : UIContext Property Name Label, Assign Button
            GUILayout.BeginHorizontal();
            var style = new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                fontSize = EditorDefines.HighlightFontSize,
            };
            style.normal.textColor = Color.white;
            GUILayout.Label(ui.GetContextType().Name, style);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(EditorDefines.FindAndAssignText, GUILayout.Width(200)))
            {
                ButtonOnClicked();
            }
            GUILayout.EndHorizontal();

            // section 2 : Context Data
            EditorGUILayout.PropertyField(context, true);

            // space
            EditorGUILayout.Space(10);

            // section 3 : Assign Button
            if (GUILayout.Button(EditorDefines.FindAndAssignText))
            {
                ButtonOnClicked();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    //private void OnValidate()
    //{
    //    Debug.Log("OnValidate");
    //    isChanged = true;
    //}

    void ButtonOnClicked()
    {
        ui.DBInfo.NotFoundObjects.Clear();
        var notFounds = ui.AutoAssignUIs();
        if (notFounds.Count != 0)
        {
            foreach (var name in notFounds) ui.DBInfo.NotFoundObjects.Add(name);
        }
        EditorUtility.SetDirty(target);
    }
}

[CustomPropertyDrawer(typeof(DBInfo))]
public class DBInfoDrawer : PropertyDrawer
{
    readonly GUIStyle titleTextStyle = new GUIStyle()
    {
        fontStyle = FontStyle.Bold,
        fontSize = EditorDefines.InfoFontSize,
    };
    bool foldout;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _ = EditorGUI.BeginProperty(position, label, property);
        SerializedProperty notFoundObjects = property.FindPropertyRelative("NotFoundObjects");

        // TODO : 아직 버튼 안눌렀을 때 메시지 표시 및 처리

        if (notFoundObjects.arraySize != 0)
        {
            titleTextStyle.normal.textColor = EditorDefines.WarningColor;
            EditorGUI.LabelField(position, EditorDefines.DBRefInfoTitle_NotFound, titleTextStyle);
            position.y += EditorGUIUtility.singleLineHeight;

            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, EditorDefines.NotFoundObjectsHeader);
            if (foldout)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                for (int i = 0; i < notFoundObjects.arraySize; i++)
                {
                    EditorGUILayout.LabelField(notFoundObjects.GetArrayElementAtIndex(i).stringValue);
                }
                EditorGUILayout.EndVertical();

            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        else
        {
            
            titleTextStyle.normal.textColor = EditorDefines.OkColor;
            EditorGUI.LabelField(position, EditorDefines.DBRefInfoTitle_AllFound, titleTextStyle);
            position.y += EditorGUIUtility.singleLineHeight;
        }

        EditorGUILayout.Space(EditorDefines.SpacePreference);

        EditorGUI.EndProperty();
    }
}
