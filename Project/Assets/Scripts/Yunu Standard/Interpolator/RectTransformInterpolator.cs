using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformInterpolator : Interpolator
{
    [SerializeField]
    RectTransform ATransform, BTransform;
    [SerializeField]
    private bool worldPosition=true;
    [SerializeField]
    VectorInterpolator.VectorSetter positionSetter;
    [SerializeField]
    VectorInterpolator.Vector2DSetter anchoredPositionSetter;
    [SerializeField]
    QuaternionInterpolator.QuaternionSetter RotationSetter;
    [SerializeField]
    VectorInterpolator.Vector2DSetter sizeDeltaSetter;
    [SerializeField]
    VectorInterpolator.Vector2DSetter pivotSetter;
    [SerializeField]
    VectorInterpolator.Vector2DSetter anchorMinSetter;
    [SerializeField]
    VectorInterpolator.Vector2DSetter anchorMaxSetter;
    public override void Setter(float point)
    {
        positionSetter.Invoke(Vector3.LerpUnclamped(
            worldPosition?ATransform.position: ATransform.localPosition,
            worldPosition?BTransform.position: BTransform.localPosition,inverselerp(point)));
        anchoredPositionSetter.Invoke(Vector2.LerpUnclamped(ATransform.anchoredPosition,BTransform.anchoredPosition,inverselerp(point)));
        RotationSetter.Invoke(Quaternion.LerpUnclamped(ATransform.localRotation, BTransform.localRotation, inverselerp(point)));
        sizeDeltaSetter.Invoke(Vector2.LerpUnclamped(ATransform.sizeDelta,BTransform.sizeDelta,inverselerp(point)));
        pivotSetter.Invoke(Vector2.LerpUnclamped(ATransform.pivot,BTransform.pivot,inverselerp(point)));
        anchorMinSetter.Invoke(Vector2.LerpUnclamped(ATransform.anchorMin,BTransform.anchorMin,inverselerp(point)));
        anchorMaxSetter.Invoke(Vector2.LerpUnclamped(ATransform.anchorMax,BTransform.anchorMax,inverselerp(point)));
    }
}
