using UnityEditor;
using TMPro;
using UnityEngine;

[CustomPropertyDrawer(typeof(TMP_TextAreaAttribute))]
public class TMP_TextAreaDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * (attribute as TMP_TextAreaAttribute).lines;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var areaAttribute = attribute as TMP_TextAreaAttribute;
        var style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;
        style.richText = true;

        EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight), label);
        property.stringValue = EditorGUI.TextArea(new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight * areaAttribute.lines), property.stringValue, style);

        EditorGUI.EndProperty();
    }
}

public class TMP_TextAreaAttribute : PropertyAttribute
{
    public readonly int lines;

    public TMP_TextAreaAttribute(int lines)
    {
        this.lines = lines;
    }
}

