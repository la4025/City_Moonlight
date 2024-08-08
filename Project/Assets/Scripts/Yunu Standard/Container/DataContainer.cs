using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class DataContainer<Data> : MonoBehaviour
{
    [Serializable]
    public class Listener : UnityEvent<Data> { };
    [SerializeField]
    private Data value;
    [SerializeField]
    private UnityEvent<Data> onApply;
    public Data Value { get { return value; } set { SetValue(value); } }
    protected virtual void SetValue(Data arg)
    {
        this.value = arg;
    }
    public void Apply()
    {
        onApply.Invoke(this.value);
    }
    //public void Apply2() { onApply.Invoke(value); }
}
