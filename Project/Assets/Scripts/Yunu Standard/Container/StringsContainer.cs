using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StringsContainer : DataContainer<string[]>
{
    [SerializeField]
    UnityEngine.Events.UnityEvent<string> stringApplier;
    public void ApplyRandomString()
    {
        stringApplier.Invoke(Value[Random.Range(0,Value.Length-1)]);
    }
}
