using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VectorInterpolator : Interpolator
{
    [SerializeField]
    private Vector3 aVector=Vector3.zero,bVector=Vector3.one;
    public Vector3 AVector { get { return aVector; } set { aVector = value; } }
    public Vector3 BVector { get { return bVector; } set { bVector = value; } }
    [Serializable]
    public class VectorSetter : UnityEvent<Vector3> { };
    [Serializable]
    public class Vector2DSetter : UnityEvent<Vector2> { };
    [SerializeField]
    VectorSetter setter;
    [SerializeField]
    Vector2DSetter setter2d;
    public override void Setter(float point)
    {
        Vector3 lerped = Vector3.LerpUnclamped(AVector, BVector, inverselerp(point));
        setter.Invoke(lerped);
        setter2d.Invoke((Vector2)lerped);
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(setter,"localScale");
    }
#endif
}
