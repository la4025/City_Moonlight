using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class LinearFloatInterpolator : Interpolator
{
    [Serializable]
    public class FloatSetter : UnityEvent<float> { };

    [SerializeField]
    private float aFloat,bFloat;
    public float AFloat { get { return aFloat; } set { aFloat = value; } }
    public float BFloat { get { return bFloat; } set { bFloat = value; } }
    [SerializeField]
    private FloatSetter setter;

    public override void Setter(float point)
    {
        float result = Mathf.LerpUnclamped(aFloat, bFloat, inverselerp(point));
        setter.Invoke(result);
    }
}
