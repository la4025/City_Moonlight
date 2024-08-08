using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

/*
 * LocalBoolsBranch는 여러 불리언값들을 캐싱할 필요가 있을 때 사용됩니다.
 * Boolean array에 저장된 값들의 관계는 AND,OR,XOR 중 하나의 논리연산으로 묶여집니다.
 * Yunu
 */
public class LocalBoolsBranch : BoolBranch
{
    [Serializable]
    protected enum BoolOperator { AND, OR, XOR }
    [SerializeField]
    private bool invokeOnValueChanged=true;
    [SerializeField]
    private bool[] localBooleans;
    [SerializeField]
    private BoolOperator operationType;

    private bool isBooleanCached = false;
    private bool cachedBoolean = false;
    public void Invoke()
    {
        Invoke(GetLocalBoolean());
    }
    private void SetLocalBoolean(int index, bool value)
    {
        if (localBooleans[index] == value)
            return;
        localBooleans[index] = value;
        isBooleanCached = false;
        if (invokeOnValueChanged)
            Invoke();
    }
    public bool GetLocalBoolean()
    {
        if (isBooleanCached)
            return cachedBoolean;
        bool returnValue = false;
        foreach (bool each in localBooleans)
        {
            if (!isBooleanCached)
            {
                returnValue = each;
                isBooleanCached = true;
                continue;
            }
            switch (operationType)
            {
                case BoolOperator.AND:
                    returnValue = each && returnValue;
                    break;
                case BoolOperator.OR:
                    returnValue = each || returnValue;
                    break;
                case BoolOperator.XOR:
                    returnValue = each != returnValue;
                    break;
            }
        }
        return cachedBoolean = returnValue;
    }
    public void SetTrue(int index)
    {
        SetLocalBoolean(index, true);
    }
    public void SetFalse(int index)
    {
        SetLocalBoolean(index, false);
    }
}
