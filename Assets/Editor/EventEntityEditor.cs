using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventEntity))]
public class EventEntityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EventEntity eventEntity = (EventEntity)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventPattern"), new GUIContent("Event Pattern"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventName"), new GUIContent("Event Name"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventDescription"), new GUIContent("Event Description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventBackground"), new GUIContent("Event Background"));

        if (eventEntity.eventPattern == EventEntity.eventPatterns.TwoButton)
        {
            DrawTwoButtonEventPanelUI(eventEntity);
        }
        else
        {
            DrawThreeButtonEventPanelUI(eventEntity);
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelButtonText"), new GUIContent("One Button Event Panel Button Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelButtonColor"), new GUIContent("One Button Event Panel Button Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelImage"), new GUIContent("One Button Event Panel Image"));

        // Other properties
        // ...

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawTwoButtonEventPanelUI(EventEntity eventEntity)
    {
        EditorGUILayout.LabelField("Two Button Event Panel UI", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelText"), new GUIContent("Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton1Text"), new GUIContent("Button 1 Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton1Color"), new GUIContent("Button 1 Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton1EventType"), new GUIContent("Button 1 Event"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton2Text"), new GUIContent("Button 2 Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton2Color"), new GUIContent("Button 2 Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton2EventType"), new GUIContent("Button 2 Event"));
        eventEntity.twoButtonEventPanelImage = (Sprite)EditorGUILayout.ObjectField("Image", eventEntity.twoButtonEventPanelImage, typeof(Sprite), false);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelTextOnTwoButton1"), new GUIContent("One Button Event Panel Text1"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelTextOnTwoButton2"), new GUIContent("One Button Event Panel Text2"));
    }

    private void DrawThreeButtonEventPanelUI(EventEntity eventEntity)
    {
        EditorGUILayout.LabelField("Three Button Event Panel UI", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelText"), new GUIContent("Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton1Text"), new GUIContent("Button 1 Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton1Color"), new GUIContent("Button 1 Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton1EventType"), new GUIContent("Button 1 Event"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton2Text"), new GUIContent("Button 2 Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton2Color"), new GUIContent("Button 2 Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton2EventType"), new GUIContent("Button 2 Event"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton3Text"), new GUIContent("Button 3 Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton3Color"), new GUIContent("Button 3 Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton3EventType"), new GUIContent("Button 3 Event"));
        eventEntity.threeButtonEventPanelImage = (Sprite)EditorGUILayout.ObjectField("Image", eventEntity.threeButtonEventPanelImage, typeof(Sprite), false);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton1InfoText"), new GUIContent("Button 1 Info Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton2InfoText"), new GUIContent("Button 2 Info Text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton3InfoText"), new GUIContent("Button 3 Info Text"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelTextOnThreeButton1"), new GUIContent("One Button Event Panel Text1"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelTextOnThreeButton2"), new GUIContent("One Button Event Panel Text2"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelTextOnThreeButton3"), new GUIContent("One Button Event Panel Text3"));

    }
}