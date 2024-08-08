using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Accumulator : MonoBehaviour
{
    [SerializeField]
    private float accumulatedScore;
    public float AccumulatedScore { get { return accumulatedScore; } protected set { accumulatedScore = value; } }
    
    [Serializable]
    public class FloatEvent : UnityEvent<float> { };

    [SerializeField]
    private FloatEvent receiver; 

    public virtual void Delta(float value)
    {
        AccumulatedScore += value;
    }
    public void ApplyValue()
    {
        receiver.Invoke(accumulatedScore);
    }
    
    void Reset()
    {
        AccumulatedScore = 0.0f;
    }
}
