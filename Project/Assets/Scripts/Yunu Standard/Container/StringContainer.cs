using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StringContainer : DataContainer<string>
{
    [Serializable]
    protected class StrListener : Listener { };
    [SerializeField]
    private StrListener onValueChange,applyListener;
    [SerializeField]
    private string format;
    protected override void SetValue(string arg)
    {
        base.SetValue(arg);
        onValueChange.Invoke(arg);
    }
    public void SetValue(Single arg)
    {
        string param;
        if (format != "")
            param = string.Format(format, arg);
        else
            param = arg.ToString();
        base.SetValue(param);
            
        onValueChange.Invoke(param);
    }
    public void SetValue(int arg)
    {
        base.SetValue(arg.ToString());
        onValueChange.Invoke(arg.ToString());
    }
    protected void OnValidate()
    {
        for (int i = 0; i < onValueChange.GetPersistentEventCount(); i++)
            onValueChange.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.EditorAndRuntime);
        onValueChange.Invoke(Value);
    }
}
