using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingTransform : MonoBehaviour
{
    // dragging factor가 작을수록 부모와의 원래 관계를 유지하려는 경향이 크다.
    [SerializeField]
    float draggingFactor = 0.5f;
    Vector3 initialLocalPosition;
    Quaternion initialLocalRotation;

    Vector3 lastWorldPos;
    Quaternion lastWorldRot;
    float localParentDistance;
    float parentDistance;

    void Start()
    {
        parentDistance = (transform.parent.transform.position - transform.position).magnitude;
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
        localParentDistance = initialLocalPosition.magnitude;
    }
    private void FixedUpdate()
    {
        //Vector3 nextLocalPosition = Vector3.Lerp(lastWorldPos, transform.position, draggingFactor);
        //nextLocalPosition += Vector3.down * Player.instance.gravity * Time.fixedDeltaTime;
        Vector3 nextLocalPosition = transform.parent.InverseTransformPoint(lastWorldPos);
        //Quaternion nextLocalRotation =transform.parent.rotat lastWorldPos;
        float overKillRatio = nextLocalPosition.magnitude / localParentDistance;
        if (overKillRatio > 1f)
        {
            nextLocalPosition *= 1 / overKillRatio;
        }
        transform.localPosition = Vector3.Lerp(initialLocalPosition, nextLocalPosition, draggingFactor);
        transform.parent.LookAt(transform);
        //transform.rotation = lastWorldRot;
        //transform.localRotation = Quaternion.Lerp(initi,draggingFactor);
        lastWorldPos = transform.position;
        lastWorldRot = transform.rotation;
    }
}
