using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTransformCaller : GlobalEntityCaller<Transform>
{
    public void AlignPosition(Transform target)
    {
        GlobalTransform.entityById[id].position = target.position;
    }
    public void AlignPositionAndRotation(Transform target)
    {
        GlobalTransform.entityById[id].position = target.position;
        GlobalTransform.entityById[id].rotation = target.rotation;
    }
}
