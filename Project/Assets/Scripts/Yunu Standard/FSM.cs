using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<StateEnum> where StateEnum : System.Enum
{
    // 심화 한걸음 더 나아가기!
    // 얘들을 private로 지정해 FSM 객체의 내부구현을 숨기고,
    // public 함수를 여럿 만들어 친절한 인터페이스를 구현해 보시오.
    public Dictionary<StateEnum, List<(StateEnum destination, Func<bool> condition)>> transitions = new Dictionary<StateEnum, List<(StateEnum destination, Func<bool> condition)>>();
    public Dictionary<StateEnum, Action> engageAction = new Dictionary<StateEnum, Action>();
    public Dictionary<StateEnum, Action> updateAction= new Dictionary<StateEnum, Action>();
    public StateEnum currentState;
    public StateEnum previousState { get; private set; }
    public FSM(StateEnum initalState)
    {
        currentState = initalState;
        foreach (StateEnum state in Enum.GetValues(typeof(StateEnum)))
        {
            transitions[state] = new List<(StateEnum destination, Func<bool> condition)>();
            engageAction[state] = () => { };
            updateAction[state] = () => { };
        }
    }
    public void UpdateState()
    {
        bool engaged = false;
        // transitionList는 하나의 상태가 다른 상태로 이동하기 위한 조건을 저장하고 있다.
        var transitionList = transitions[currentState];
        // transitionList의 모든 전환조건을 검사하며, 충족되는 전환 조건이 있을 경우 현재상태를 전환한다.
        // foreach는 c++의 range based for와 같다
        foreach (var eachTransition in transitionList)
        {
            if (eachTransition.condition() == true)
            {
                previousState = currentState; 
                currentState = eachTransition.destination;
                engaged = true;
                break;
            }
        }
        if (engaged == true)
            engageAction[currentState]();

        updateAction[currentState]();
    }
}
