//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(TimerModule))]
//[CanEditMultipleObjects]
//public class TimerModuleEditor : Editor
//{
//    SerializedProperty duration;
//    SerializedProperty playrate;
//    SerializedProperty isTimeScaled;
//    SerializedProperty isTimeNormalized;
//    SerializedProperty timeCurve;
//    SerializedProperty updateAction;

//    private void OnEnable()
//    {
//        serializedObject.FindProperty("duration");
//        serializedObject.FindProperty("playrate");
//        serializedObject.FindProperty("isTimeScaled");
//        serializedObject.FindProperty("isTimeNormalized");
//        serializedObject.FindProperty("timeCurve");
//        serializedObject.FindProperty("updateAction");
//    }
//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        EditorGUILayout.PropertyField(duration);
//        EditorGUILayout.PropertyField(playrate);
//        EditorGUILayout.PropertyField(isTimeScaled);
//        EditorGUILayout.PropertyField(isTimeNormalized);
//        EditorGUILayout.PropertyField(timeCurve);
//        EditorGUILayout.PropertyField(updateAction);
//        serializedObject.ApplyModifiedProperties();

//        base.OnInspectorGUI();
//    }
//}
