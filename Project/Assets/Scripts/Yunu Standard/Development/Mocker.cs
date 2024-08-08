using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Mocker : MonoBehaviour
{
    public UnityEvent Mockery;
    [SerializeField]
    private bool InvokeAgain;
    void OnValidate()
    {
        if (InvokeAgain)
        {
            InvokeAgain = false;
            Mockery.Invoke();
        }
#if UNITY_EDITOR
        UnityEditor.Events.AutoRegistration.AutoAttachMethod(Mockery, "Invoke");
#endif
    }
    public void DebugMessage(string message)
    {
        Debug.Log(message);
    }
}
