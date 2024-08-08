using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransformInterpolator : Interpolator
{
    [SerializeField]
    Transform ATransform, BTransform;
    [SerializeField]
    UnityEvent<Vector3> position ;
    [SerializeField]
    UnityEvent<Quaternion> rotation;
    [SerializeField]
    UnityEvent<Vector3> localScale;
    public override void Setter(float point)
    {
        position.Invoke(Vector3.LerpUnclamped(ATransform.position,BTransform.position,inverselerp(point)));
        rotation.Invoke(Quaternion.LerpUnclamped(ATransform.rotation,BTransform.rotation,inverselerp(point)));
        localScale.Invoke(Vector3.LerpUnclamped(ATransform.localScale,BTransform.localScale,inverselerp(point)));
    }
    private void Reset()
    {
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(position, "position");
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(rotation, "rotation");
        UnityEditor.Events.AutoRegistration.AutoAttachPersistentMethod(localScale, "localScale");
    }
#endif
}
