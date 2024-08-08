using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class StringIterator : MonoBehaviour
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }
    public StringEvent onIteration;
    public List<string> strings;
    public int index;

    public void Iterate()
    {
        index=(index+1)%strings.Count;
        onIteration.Invoke(strings[index]);
    }
}
