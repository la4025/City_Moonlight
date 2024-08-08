using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorInterpolator : Interpolator
{
    [SerializeField]
    private Color AColor, BColor;
    public Color A_Color { set { AColor = value; } get { return AColor; } }
    public Color B_Color { set { BColor = value; } get { return BColor; } }
    [Serializable]
    public class ColorSetter : UnityEvent<Color> { };

    [SerializeField]
    private ColorSetter setters = new ColorSetter();
    public override void Setter(float point)
    {
        setters.Invoke(Color.LerpUnclamped(AColor, BColor, inverselerp(point)));
    }
    private void Reset()
    {
        AColor = new Color(1,1,1,0);
        BColor = new Color(1,1,1,1);
    }
#if UNITY_EDITOR
    protected void OnValidate()
    {
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(setters, "color");
    }
#endif
}
