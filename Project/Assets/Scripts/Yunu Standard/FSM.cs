using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<StateEnum> where StateEnum : System.Enum
{
    // ��ȭ �Ѱ��� �� ���ư���!
    // ����� private�� ������ FSM ��ü�� ���α����� �����,
    // public �Լ��� ���� ����� ģ���� �������̽��� ������ ���ÿ�.
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
        // transitionList�� �ϳ��� ���°� �ٸ� ���·� �̵��ϱ� ���� ������ �����ϰ� �ִ�.
        var transitionList = transitions[currentState];
        // transitionList�� ��� ��ȯ������ �˻��ϸ�, �����Ǵ� ��ȯ ������ ���� ��� ������¸� ��ȯ�Ѵ�.
        // foreach�� c++�� range based for�� ����
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
