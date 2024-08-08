using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class FloatBranch : MonoBehaviour
{
    [Serializable]
    public class EventPerRange
    {
        public float min;
        public float max;
        public UnityEvent<float> rangeEvent;
    }
    [SerializeField]
    private EventPerRange[] eventsPerRange;
    public float localValue { get; set; } = 0;
    public void Invoke()
    {
        Invoke(localValue);
    }
    public void Invoke(float value)
    {
        foreach (var each in eventsPerRange)
            if (each.min < value && value < each.max)
                each.rangeEvent.Invoke(value);
    }

}
