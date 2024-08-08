using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onContact;
    [SerializeField]
    private Collider specificTarget;
    private void OnCollisionEnter(Collision collision)
    {
        if (specificTarget != null)
        {
            if (specificTarget == collision.collider)
                onContact.Invoke();
        }
        else
        {
            onContact.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (specificTarget != null)
        {
            if (specificTarget == other)
                onContact.Invoke();
        }
        else
        {
            onContact.Invoke();
        }
    }
}
