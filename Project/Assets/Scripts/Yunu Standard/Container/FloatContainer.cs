using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FloatContainer : DataContainer<float>
{
    [Serializable]
    protected class FloatListener : Listener { }
    [SerializeField]
    protected FloatListener beforeChangeValue, afterChangeValue, applyListener;
    protected override void SetValue(float arg)
    {
        beforeChangeValue.Invoke(Value);
        OnBeforeChangeValue(Value);
        base.SetValue(arg);
        afterChangeValue.Invoke(arg);
        OnAfterChangeValue(Value);
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(beforeChangeValue, "Invoke");
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(afterChangeValue, "Invoke");
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(applyListener, "Invoke");
    }
#endif
    public void AddDelta(float delta)
    {
        Value += delta;
    }
    protected virtual void OnBeforeChangeValue(float arg) { }
    protected virtual void OnAfterChangeValue(float arg) { }
}
