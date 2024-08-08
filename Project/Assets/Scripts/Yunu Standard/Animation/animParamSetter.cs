using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class animParamSetter : MonoBehaviour
{
    [Serializable]
    public class SerializedAnimParam<ValueType>
    {
        public SerializedAnimParam(string name, ValueType value)
        {
            this.name = name;
            this.value = value;
        }
        public string name;
        public ValueType value;
    }
    [SerializeField]
    Animator targetAnimator;
    [SerializeField]
    bool resetOnStart = true;
    [SerializeField]
    List<SerializedAnimParam<bool>> boolParams= new List<SerializedAnimParam<bool>>();
    [SerializeField]
    List<SerializedAnimParam<float>> floatParams = new List<SerializedAnimParam<float>>();
    [SerializeField]
    List<SerializedAnimParam<int>> intParams = new List<SerializedAnimParam<int>>();
    [SerializeField]
    List<SerializedAnimParam<bool>> triggers = new List<SerializedAnimParam<bool>>();
    void Start()
    {
        ResetParams();
    }
    public void ResetParams()
    {
        foreach (var each in boolParams)
            targetAnimator.SetBool(each.name, each.value);
        foreach (var each in floatParams)
            targetAnimator.SetFloat(each.name, each.value);
        foreach (var each in intParams)
            targetAnimator.SetInteger(each.name, each.value);
        foreach (var each in triggers)
            if (each.value)
                targetAnimator.SetTrigger(each.name);
            else
                targetAnimator.ResetTrigger(each.name);
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        targetAnimator = GetComponent<Animator>();
        AnimatorControllerParameter[] paramsList = null;
        if (targetAnimator)
            paramsList = targetAnimator.parameters;
        if (paramsList != null)
        {
            foreach (var each in paramsList)
            {
                switch (each.type)
                {
                    case AnimatorControllerParameterType.Bool:
                        if (boolParams.Find(item => item.name == each.name) == null)
                            boolParams.Add(new SerializedAnimParam<bool>(each.name, each.defaultBool));
                        break;
                    case AnimatorControllerParameterType.Float:
                        if (floatParams.Find(item => item.name == each.name) == null)
                            floatParams.Add(new SerializedAnimParam<float>(each.name, each.defaultFloat));
                        break;
                    case AnimatorControllerParameterType.Int:
                        if (intParams.Find(item => item.name == each.name) == null)
                            intParams.Add(new SerializedAnimParam<int>(each.name, each.defaultInt));
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        if (triggers.Find(item => item.name == each.name) == null)
                            triggers.Add(new SerializedAnimParam<bool>(each.name, each.defaultBool));
                        break;
                }
            }
        }
    }
#endif

}
