using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(WallGenerator)), CanEditMultipleObjects]
public class WallGeneratorEditor : Editor
{
	public SerializedProperty levelTreeProp;
	public SerializedProperty roomsProp;

	public void OnEnable ()
	{
		levelTreeProp = serializedObject.FindProperty ("levelTree");
		roomsProp = serializedObject.FindProperty ("rooms");
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		levelTreeProp.stringValue = EditorGUILayout.TextArea (levelTreeProp.stringValue, GUILayout.MaxHeight (75));

		serializedObject.ApplyModifiedProperties ();
	}
}
