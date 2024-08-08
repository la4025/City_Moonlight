using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/*
 * BoolBranch는 참이 입력으로 들어올 경우에 실행될 콜백과 거짓이 입력으로 들어올 경우 실행될 콜백을 각각 필드로 가지고 있습니다.
 * BoolBranch는 분기에 따라 실행될 동작을 유니티 인스팩터에서 정의할 수 있는 조건문과 같습니다.
 * 불리언 값은 Invoke 함수의 매개변수로 전달되며, 이 매개변수에 따라 서로 다른 콜백이 호출됩니다.
 * BoolBranch 클래스는 AutoRegistration이 어떻게 활용될 수 있는지에 대한 좋은 예제가 될 수 있습니다.
 * 아래의 OnValidate 함수에서 Invoke 함수를 어떻게 자동으로 찾아 할당하는지 확인하세요.
 */
public class BoolBranch : MonoBehaviour
{
    [SerializeField]
    private UnityEvent trueEvent, falseEvent;
    // Start is called before the first frame update
    public void Invoke(float falseIfZero)
    {
        Invoke(falseIfZero != 0);
    }
    public void Invoke(bool arg = false)
    {
        if (arg)
            trueEvent.Invoke();
        else
            falseEvent.Invoke();
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        // trueEvent의 콜백에 게임오브젝트가 할당될 경우, 해당 게임 오브젝트에 할당된 
        // 모든 Monobehaviour 객체들을 뒤져 Invoke함수가 있는지 찾아냅니다.
        // Invoke라는 이름의 함수가 발견될 경우, 해당 함수를 호출하는 액션을 이벤트에 추가합니다.
        UnityEditor.Events.AutoRegistration.AutoAttachMethod(trueEvent, "Invoke");
        UnityEditor.Events.AutoRegistration.AutoAttachMethod(falseEvent, "Invoke");
    }
#endif
}
