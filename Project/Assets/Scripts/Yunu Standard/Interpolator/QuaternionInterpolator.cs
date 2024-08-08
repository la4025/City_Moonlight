using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuaternionInterpolator : Interpolator
{
    [SerializeField]
    private Quaternion aQuaternion,bQuaternion;
    public Quaternion AQuaternion { get { return aQuaternion; } set { aQuaternion = value; } }
    public Quaternion BQuaternion { get { return bQuaternion; } set { bQuaternion = value; } }
    [Serializable]
    public class QuaternionSetter : UnityEvent<Quaternion> { };
    [SerializeField]
    QuaternionSetter setter;
    public override void Setter(float point)
    {
        setter.Invoke(Quaternion.LerpUnclamped(AQuaternion,BQuaternion,inverselerp(point)));
    }
}
