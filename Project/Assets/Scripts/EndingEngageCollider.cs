using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingEngageCollider : MonoBehaviour
{
    public UnityEvent engageEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other == Player.instance.triggerCollider)
            engageEvent.Invoke();
    }
}
