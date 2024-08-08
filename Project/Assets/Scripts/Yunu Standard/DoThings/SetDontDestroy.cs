using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDontDestroy : MonoBehaviour
{
    [SerializeField]
    bool setOnStart = true;
    private void Start()
    {
        if (setOnStart)
            DontDestroyOnLoad(gameObject);
    }
    public void DontDestroyOnLoad()
    {
        DontDestroyOnLoad(gameObject);
    }
}
