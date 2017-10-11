using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(HexCoordinate))]
public class HexCoordinateDrawer : PropertyDrawer {


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);

        HexCoordinate coordinates = new HexCoordinate(
                property.FindPropertyRelative("x").intValue,
                property.FindPropertyRelative("z").intValue
            );

        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, coordinates.ToString());

    }

}
