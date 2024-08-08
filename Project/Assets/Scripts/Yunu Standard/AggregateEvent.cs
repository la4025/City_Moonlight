using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AggregateEvent : MonoBehaviour
{
    [SerializeField]
    UnityEvent aggregation;
    public void StartEvents()
    {
        Invoke();
    }
    public void Invoke()
    {
        aggregation.Invoke();
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (transform.parent)
            if (transform.GetSiblingIndex() != transform.parent.childCount - 1)
                UnityEditor.Events.AutoRegistration.AutoAttachMethodWithObject(aggregation, "ChangeView", transform.parent.GetChild(transform.GetSiblingIndex() + 1));
        UnityEditor.Events.AutoRegistration.AutoAttachMethod(aggregation, "Invoke");
    }
#endif
}
