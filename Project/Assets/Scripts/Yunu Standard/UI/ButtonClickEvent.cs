using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField] UnityEvent onClick;
    [SerializeField] KeyCode[] keys;
    void Update()
    {
        foreach (var each in keys)
            if (Input.GetKeyDown(each))
                onClick.Invoke();
    }
}
