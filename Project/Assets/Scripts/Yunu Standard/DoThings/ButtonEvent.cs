using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : MonoBehaviour
{
    [Serializable]
    enum InputType
    {
        Button,
        ButtonDown,
        ButtonUp
    }
    [SerializeField]
    private string buttonName;
    [SerializeField]
    private InputType inputType;
    [SerializeField]
    private UnityEvent buttonEvent;

    void Update()
    {
        bool shouldHappen = false;
        switch (inputType)
        {
            case InputType.Button:
                shouldHappen = Input.GetButton(buttonName);
                break;
            case InputType.ButtonDown:
                shouldHappen = Input.GetButtonDown(buttonName);
                break;
            case InputType.ButtonUp:
                shouldHappen = Input.GetButtonUp(buttonName);
                break;
            default:
                break;
        }
        if (shouldHappen)
            buttonEvent.Invoke();
    }
}
