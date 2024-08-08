using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class KeepRelativeOffset : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool keepX, keepY, keepZ;
    private float xDis, yDis, zDis;
    void Start()
    {
        Vector3 right = transform.parent ? transform.parent.right : Vector3.right;
        Vector3 up = transform.parent ? transform.parent.up : Vector3.up;
        Vector3 forward = transform.parent ? transform.parent.forward : Vector3.forward;
        xDis = Vector3.Dot(right, transform.position - target.position);
        yDis = Vector3.Dot(up, transform.position - target.position);
        zDis = Vector3.Dot(forward, transform.position - target.position);
    }
    void Update()
    {
        Vector3 right = transform.parent ? transform.parent.right : Vector3.right;
        Vector3 up = transform.parent ? transform.parent.up : Vector3.up;
        Vector3 forward = transform.parent ? transform.parent.forward : Vector3.forward;
        if (keepX)
            transform.position += right *
                (xDis - Vector3.Dot(transform.position - target.position, right));
        if (keepY)
            transform.position += up *
                (yDis - Vector3.Dot(transform.position - target.position, up));
        if (keepZ)
            transform.position += forward *
                (zDis - Vector3.Dot(transform.position - target.position, forward));
    }
}
