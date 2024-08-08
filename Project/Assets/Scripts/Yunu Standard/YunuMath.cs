using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class YunuMath
{
    public static float InverseEulerMethod(Func<float, float> derivative, Func<float, float> func, float outcome, float t0 = 0, int trials = 5)
    {
        float t = t0;
        for (int i = 0; i < trials; i++)
        {
            t += (outcome - func(t)) / derivative(t);
        }
        return t; 
    }
}
