using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class SplitLineAttribute : PropertyAttribute
{
    public const float defaultHeight = 2.0f;
    public const EColor defaultColor = EColor.Gray;

    public float Height { get; private set; }
    public EColor Color { get; private set; }

    public SplitLineAttribute (float height = defaultHeight, EColor color = defaultColor)
    {
        Height = height;
        Color = color;
    }
}
