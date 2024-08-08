using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignAxisOverTime : MonoBehaviour
{
    public enum Axis
    {
        Forward,
        Right,
        Up
    }
    [SerializeField] TimerModule aligningTimer;
    [SerializeField] Axis aligningAxis = Axis.Forward;
    [SerializeField] Axis rotatingAxis = Axis.Up;
    Vector3 originalAxis;
    Quaternion originalRotation;
    Vector3 rotAxis
    {
        get
        {
            switch (rotatingAxis)
            {
                case Axis.Forward:
                    return Vector3.forward;
                case Axis.Up:
                    return Vector3.up;
                case Axis.Right:
                    return Vector3.right;
                default:
                    return Vector3.zero;
            }
        }
    }
    Quaternion zyAxisMatch()
    {
        (Axis aligningAxis, Axis rotatingAxis) axisInfos = (aligningAxis, rotatingAxis);
        switch (axisInfos)
        {
            case (Axis.Up, Axis.Right):
                return Quaternion.Inverse(Quaternion.Euler(-90, 0, 180));
            default:
                return Quaternion.identity;
        }
    }
    //const Dictionary<Axis, float> a= { {Axis.Right,1f } };
    Transform focusTarget;
    private void Start()
    {
        aligningTimer.updateAction = new UnityEngine.Events.UnityAction<float>(alpha =>
        {
            Vector3 right = transform.right;
            Vector3 forward = transform.forward;
            Vector3 up = transform.up;
            switch (aligningAxis)
            {
                case Axis.Forward:
                    forward = Vector3.Lerp(originalAxis, (-transform.position + focusTarget.position).normalized, alpha);
                    break;
                case Axis.Right:
                    right = Vector3.Lerp(originalAxis, (-transform.position + focusTarget.position).normalized, alpha);
                    forward = Vector3.Cross(right,up);
                    break;
                case Axis.Up:
                    up = Vector3.Lerp(originalAxis, (-transform.position + focusTarget.position).normalized, alpha);
                    forward = Vector3.Cross(right,up);
                    break;
            }
                    transform.rotation = Quaternion.LookRotation(forward, up);
        });
    }
    public void Align(Component target)
    {
        var focusTarget = target.transform;
        if (rotatingAxis == aligningAxis)
            return;
        originalAxis = transform.forward;
        originalRotation = transform.rotation;
        switch (aligningAxis)
        {
            case Axis.Forward:
                originalAxis = transform.forward;
                break;
            case Axis.Right:
                originalAxis = transform.right;
                break;
            case Axis.Up:
                originalAxis = transform.up;
                break;
        }

        this.focusTarget = focusTarget;
        StopCoroutine(aligningTimer.StartTimer());
        StartCoroutine(aligningTimer.StartTimer());
    }
}
