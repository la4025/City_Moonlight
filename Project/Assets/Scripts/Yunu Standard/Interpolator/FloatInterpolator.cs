using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FloatInterpolator : Interpolator
{
    [Serializable]
    public class FloatSetter : UnityEvent<float> { };
    [SerializeField]
    AnimationCurve FloatCurve;
    [SerializeField]
    private FloatSetter setter;
    [SerializeField]
    bool moduloTime=false;
    public override void Setter(float point)
    {
        if (point > FloatCurve.keys.Last().time)
            point %= FloatCurve.keys.Last().time;
        setter.Invoke(FloatCurve.Evaluate(point));
    }
    private void Reset()
    {
        FloatCurve = new AnimationCurve();
        FloatCurve.AddKey(new Keyframe(1, 1,1,1));
        FloatCurve.AddKey(new Keyframe(0, 0,1,1));
    }
}
