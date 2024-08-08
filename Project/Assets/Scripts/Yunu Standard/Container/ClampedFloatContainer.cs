using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClampedFloatContainer : FloatContainer
{
    [SerializeField]
    private UnityEvent onMinReached, onMaxReached;
    [SerializeField]
    protected float min, max;
    private float last_value;
    protected override void SetValue(float arg)
    {
        base.SetValue(Mathf.Clamp(arg, min, max));

        if (Value == min && last_value != min)
            onMinReached.Invoke();
        if (Value == max && last_value != max)
            onMaxReached.Invoke();

        last_value = Value;
    }
    public float ValueProportion
    {
        get { return (Value - min) / (max - min); }
        set { SetValue(min + (max - min) * value); }
    }
    public float DeltaProportion
    {
        set { ValueProportion += value; }
    }
}
