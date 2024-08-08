using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class IntBranch : MonoBehaviour
{
    [Serializable]
    public class IntEvent : UnityEvent<int> { };
    [Serializable] public class EventPerInt { public int number; public UnityEvent Event; }
    [Tooltip("Default Event is always called regardless of argument value")]
    [SerializeField] private IntEvent defaultEvent;
    [Tooltip("Alternate Event is called when argument matches no argument.")]
    [SerializeField] private IntEvent alternateEvent;
    //default event fires anytime.
    [SerializeField] private EventPerInt[] events;


    // Start is called before the first frame update
    public void Invoke(int arg)
    {
        defaultEvent.Invoke(arg);
        foreach (EventPerInt each in events)
        {
            if (each.number == arg)
            {
                each.Event.Invoke();
                return;
            }
        }
        alternateEvent.Invoke(arg);
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        AutoRegistration.AutoAttachMethod(defaultEvent, "Invoke");
        AutoRegistration.AutoAttachMethod(alternateEvent, "Invoke");
        AutoRegistration.AutoAttachPersistentMethod(defaultEvent, "Invoke");
        AutoRegistration.AutoAttachPersistentMethod(alternateEvent, "Invoke");
        foreach (var each in events)
            AutoRegistration.AutoAttachPersistentMethod(each.Event, "Invoke");
#endif
    }
}