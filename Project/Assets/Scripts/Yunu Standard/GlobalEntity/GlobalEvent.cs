using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvent : GlobalEntity<UnityEvent>
{
    public void Invoke()
    {
        entity.Invoke();
    }
}
