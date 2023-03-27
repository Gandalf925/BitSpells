// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(EventEntity))]
// public class EventEntityEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         EventEntity eventEntity = (EventEntity)target;

//         EditorGUILayout.PropertyField(serializedObject.FindProperty("eventPattern"), new GUIContent("Event Pattern"));

//         EditorGUILayout.PropertyField(serializedObject.FindProperty("eventName"), new GUIContent("Event Name"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("eventDescription"), new GUIContent("Event Description"));

//         if (eventEntity.eventPattern == EventEntity.eventPatterns.TwoButton)
//         {
//             DrawTwoButtonEventPanelUI(eventEntity);
//         }
//         else
//         {
//             DrawThreeButtonEventPanelUI(eventEntity);
//         }

//         EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelText"), new GUIContent("One Button Event Panel Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelButtonText"), new GUIContent("One Button Event Panel Button Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelButtonColor"), new GUIContent("One Button Event Panel Button Color"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("oneButtonEventPanelImage"), new GUIContent("One Button Event Panel Image"));

//         // Other properties
//         // ...

//         serializedObject.ApplyModifiedProperties();
//     }

//     private void DrawTwoButtonEventPanelUI(EventEntity eventEntity)
//     {
//         EditorGUILayout.LabelField("Two Button Event Panel UI", EditorStyles.boldLabel);

//         EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelText"), new GUIContent("Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton1Text"), new GUIContent("Button 1 Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton1Color"), new GUIContent("Button 1 Color"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton2Text"), new GUIContent("Button 2 Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("twoButtonEventPanelButton2Color"), new GUIContent("Button 2 Color"));
//         eventEntity.twoButtonEventPanelImage = (Sprite)EditorGUILayout.ObjectField("Image", eventEntity.twoButtonEventPanelImage, typeof(Sprite), false);
//     }

//     private void DrawThreeButtonEventPanelUI(EventEntity eventEntity)
//     {
//         EditorGUILayout.LabelField("Three Button Event Panel UI", EditorStyles.boldLabel);

//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelText"), new GUIContent("Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton1Text"), new GUIContent("Button 1 Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton1Color"), new GUIContent("Button 1 Color"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton2Text"), new GUIContent("Button 2 Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton2Color"), new GUIContent("Button 2 Color"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton3Text"), new GUIContent("Button 3 Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton3Color"), new GUIContent("Button 3 Color"));
//         eventEntity.threeButtonEventPanelImage = (Sprite)EditorGUILayout.ObjectField("Image", eventEntity.threeButtonEventPanelImage, typeof(Sprite), false);

//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton1InfoText"), new GUIContent("Button 1 Info Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton2InfoText"), new GUIContent("Button 2 Info Text"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("threeButtonEventPanelButton3InfoText"), new GUIContent("Button 3 Info Text"));

//         EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType1"), new GUIContent("Event Type 1"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType2"), new GUIContent("Event Type 2"));
//         EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType3"), new GUIContent("Event Type 3"));
//     }
// }