using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameObject : GlobalEntity<GameObject>
{
    [SerializeField]
    bool dontDestroyOnLoad=true;
    protected override void Awake()
    {
        base.Awake();
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
