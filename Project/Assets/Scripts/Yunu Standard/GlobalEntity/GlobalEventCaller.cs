using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventCaller : GlobalEntityCaller<UnityEvent>
{
    public void Invoke()
    {
        GlobalEvent.entityById[id].Invoke();
    }
    public void InvokeGlobalEvent()
    {
        Invoke();
    }
}
