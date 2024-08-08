using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * BoolBranch와 다르게, LocalBoolBranch는 지역 필드 localBoolean을 가지고 있습니다. 
 * 매개변수 없이 LocalBoolBranch의 Invoke를 호출할 경우, BoolBranch의 동작을 인스턴스가 현재 갖고 있는 localBoolean을 매개변수로 동작합니다. 
 */
public class LocalBoolBranch : BoolBranch
{
    [SerializeField]
    private bool localBoolean;
    public bool LocalBoolean { get { return localBoolean; } set { localBoolean = value; } }
    public void Invoke()
    {
        Invoke(localBoolean);
    }
    public void ToggleLocalBoolean()
    {
        localBoolean = !localBoolean;
    }
}
