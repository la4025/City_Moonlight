using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VectorContainer : DataContainer<Vector3>
{
    [Serializable]
    protected class VectorListener : Listener { }
    [SerializeField]
    protected VectorListener beforeChangeValue, afterChangeValue, applyListener;
    protected override void SetValue(Vector3 arg)
    {
        beforeChangeValue.Invoke(arg);
        base.SetValue(arg);
        afterChangeValue.Invoke(arg);
    }
    public void AddDelta(Vector3 delta)
    {
        Value += delta;
    }
}
