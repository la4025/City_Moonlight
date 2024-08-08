using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class CountEvent : MonoBehaviour
{
    [Serializable]
    public class EventPerCount
    {
        public int reach;
        public UnityEvent reachEvent;
    }
    [SerializeField]
    private int count;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            foreach (var each in countEvents)
                if (count == each.reach)
                    each.reachEvent.Invoke();
        }
    }
    [SerializeField]
    private EventPerCount[] countEvents;
    public void Increment() {
        Count++;
    }
}
