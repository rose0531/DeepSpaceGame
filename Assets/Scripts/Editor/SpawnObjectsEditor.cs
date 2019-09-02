using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SpawnObjects))]
public class SpawnObjectsEditor : Editor {

    private SpawnObjects spawnObjects;

    // Variables used for when you want to spawn a Tile
    public SerializedProperty tileTypeProp, objectTypeProp, roomLayoutProp;


    private void OnEnable()
    {
        spawnObjects = (SpawnObjects)target;
        tileTypeProp = serializedObject.FindProperty("tileType");
        objectTypeProp = serializedObject.FindProperty("objectType");
        roomLayoutProp = serializedObject.FindProperty("roomLayout");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(objectTypeProp);

        SpawnObjects.ObjectType objType = (SpawnObjects.ObjectType)objectTypeProp.enumValueIndex;

        switch (objType)
        {
            case SpawnObjects.ObjectType.Tile: // Tile
                EditorGUILayout.PropertyField(tileTypeProp, new GUIContent("Type"));
                break;
            case SpawnObjects.ObjectType.RoomLayout: // Room Layout
                EditorGUILayout.PropertyField(roomLayoutProp, new GUIContent("Layout"));
                break;
            case SpawnObjects.ObjectType.Enemy: // Enemy
                EditorGUILayout.PrefixLabel("Enemy");
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
