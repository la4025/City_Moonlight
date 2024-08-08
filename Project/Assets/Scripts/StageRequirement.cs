using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageRequirement : MonoBehaviour
{
    public Collider collider;
    public UnityEvent onAcquisition;
    private void Start()
    {
        Physics.IgnoreCollision(collider, Player.instance.physicsCollider);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other != Player.instance.triggerCollider)
            return;
        StageManager.instance.stageRequirementsAccquired++;
        onAcquisition.Invoke();
        collider.enabled = false;
        if (StageManager.instance.StageLevel == 1)
            GlobalSounds.PlayRandomSound("light off");
        if (StageManager.instance.StageLevel == 2)
            GlobalSounds.PlayRandomSound("star");
        if (StageManager.instance.StageLevel == 3)
            GlobalSounds.PlayRandomSound("star");
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        collider = GetComponent<Collider>();
    }
#endif
}
