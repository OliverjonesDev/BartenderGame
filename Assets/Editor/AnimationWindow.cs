using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

//Animation editor class made by Olly - Student Number: 33697643

[CustomEditor(typeof(AnimatableXYZ)),CanEditMultipleObjects]
public class AnimationWindow : Editor
{
    private AnimatableXYZ _animatable;
    private SerializedProperty list;
    private bool showAnimValues;
    private bool showAnchorVariables;
    public override void OnInspectorGUI()
    {
        _animatable = target as AnimatableXYZ;
        list = serializedObject.FindProperty("gameObjectAnchors");
        EditorGUILayout.LabelField("Animation Tooling",EditorStyles.boldLabel);
       serializedObject.FindProperty("resetAfterInteraction").boolValue = GUILayout.Toggle(serializedObject.FindProperty("resetAfterInteraction").boolValue ,"Reset after animation finsh");
        serializedObject.FindProperty("usingGameObjectAnchors").boolValue = GUILayout.Toggle(serializedObject.FindProperty("usingGameObjectAnchors").boolValue, " Anchor Animation");
        serializedObject.FindProperty("interactedWith").boolValue = GUILayout.Toggle(serializedObject.FindProperty("interactedWith").boolValue, " Debug - Interacted With");
        _animatable.exitInteraction = GUILayout.Toggle(_animatable.exitInteraction, " Debug - Exit Interaction");
        if (!serializedObject.FindProperty("usingGameObjectAnchors").boolValue)
        {
            serializedObject.FindProperty("x").boolValue = GUILayout.Toggle(serializedObject.FindProperty("x").boolValue, " X Animation");
            serializedObject.FindProperty("y").boolValue = GUILayout.Toggle(serializedObject.FindProperty("y").boolValue, " Y Animation");
            serializedObject.FindProperty("z").boolValue = GUILayout.Toggle(serializedObject.FindProperty("z").boolValue, " Z Animation");
            if (serializedObject.FindProperty("x").boolValue)
            {
                serializedObject.FindProperty("usingGameObjectAnchors").boolValue = false;
                EditorGUILayout.LabelField("X Animation Distance");
                serializedObject.FindProperty("xDistanceMin").floatValue = EditorGUILayout.FloatField("Distance Min",serializedObject.FindProperty("xDistanceMin").floatValue);
                serializedObject.FindProperty("xDistanceMax").floatValue = EditorGUILayout.FloatField("Distance Max",serializedObject.FindProperty("xDistanceMax").floatValue);
                serializedObject.FindProperty("xDistance").floatValue = EditorGUILayout.Slider(_animatable.xDistance,serializedObject.FindProperty("xDistanceMin").floatValue,serializedObject.FindProperty("xDistanceMax").floatValue);
            }
            if (serializedObject.FindProperty("y").boolValue)
            {        
                serializedObject.FindProperty("usingGameObjectAnchors").boolValue = false;
                EditorGUILayout.LabelField("Y Animation Distance");
                serializedObject.FindProperty("yDistanceMin").floatValue = EditorGUILayout.FloatField("Distance Min", serializedObject.FindProperty("yDistanceMin").floatValue);
                serializedObject.FindProperty("yDistanceMax").floatValue = EditorGUILayout.FloatField("Distance Max", serializedObject.FindProperty("yDistanceMax").floatValue);
                serializedObject.FindProperty("yDistance").floatValue = EditorGUILayout.Slider(serializedObject.FindProperty("yDistance").floatValue, serializedObject.FindProperty("yDistanceMin").floatValue, serializedObject.FindProperty("yDistanceMax").floatValue);
            }
            if (serializedObject.FindProperty("z").boolValue)
            {        
                serializedObject.FindProperty("usingGameObjectAnchors").boolValue = false;
                EditorGUILayout.LabelField("Z Animation Distance");
                serializedObject.FindProperty("zDistanceMin").floatValue = EditorGUILayout.FloatField("Distance Min", serializedObject.FindProperty("zDistanceMin").floatValue);
                serializedObject.FindProperty("zDistanceMax").floatValue = EditorGUILayout.FloatField("Distance Max", serializedObject.FindProperty("zDistanceMax").floatValue);
                serializedObject.FindProperty("zDistance").floatValue = EditorGUILayout.Slider(serializedObject.FindProperty("zDistance").floatValue, serializedObject.FindProperty("zDistanceMin").floatValue, serializedObject.FindProperty("zDistanceMax").floatValue);
            }
        }
        else
        {
            showAnchorVariables = EditorGUILayout.Foldout(showAnchorVariables, "Anchor Values",true);
            if (showAnchorVariables)
            {
                EditorGUILayout.PropertyField(list, true);
            }
            serializedObject.FindProperty("x").boolValue = false;
            serializedObject.FindProperty("y").boolValue = false;
            serializedObject.FindProperty("z").boolValue = false;
        }
        
        showAnimValues = EditorGUILayout.Foldout(showAnimValues,"Animation Speed",true);
        if (showAnimValues)
        {
            serializedObject.FindProperty("speedCurve").animationCurveValue = EditorGUILayout.CurveField("Animation Curve", _animatable.speedCurve);
        }
        serializedObject.FindProperty("interactionSound").objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField("Audio for Open", _animatable.interactionSound,typeof(AudioClip),false);
        serializedObject.FindProperty("interactionExitSound").objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField("Audio for Close", _animatable.interactionSound,typeof(AudioClip),false);
        serializedObject.ApplyModifiedProperties();

        
        
        
    }
    
}
