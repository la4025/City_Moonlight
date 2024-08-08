using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimParam : MonoBehaviour
{
    [SerializeField]Animator target;
    [SerializeField]string paramName;
    
    public void SetBool(bool value)
    {
        target.SetBool(paramName,value);
    }
    public void SetFloat(float value)
    {
        target.SetFloat(paramName,value);
    }
    public void SetInteger(int value)
    {
        target.SetInteger(paramName,value);
    }
    private void Reset()
    {
        target = GetComponent<Animator>();
    }
}
