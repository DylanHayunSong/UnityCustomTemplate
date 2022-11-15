using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SplitLineAttribute))]
public class SplitLineDrawer : DecoratorDrawer
{
    public override float GetHeight ()
    {
        SplitLineAttribute lineAtt = (SplitLineAttribute)attribute;
        return EditorGUIUtility.singleLineHeight * lineAtt.Height;
    }

    public override void OnGUI (Rect position)
    {
        Rect rect = EditorGUI.IndentedRect(position);
        rect.y += EditorGUIUtility.singleLineHeight / 3.0f;
        SplitLineAttribute lineAtt = (SplitLineAttribute)attribute;
        rect.height = lineAtt.Height;
        EditorGUI.DrawRect(rect, lineAtt.Color.GetColor());
    }
}
