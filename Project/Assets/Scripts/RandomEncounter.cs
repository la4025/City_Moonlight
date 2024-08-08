using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class RandomEncounter<T>
//{
//    private static Dictionary<T, float> list;
//    private float total;

//    public void Add(T type, float weight)
//    {
//        if (!list.ContainsKey(type))
//        {
//            list.Add(type, weight);
//            totalWeightUpdate();
//        }
//    }

//    public void Remove(T type)
//    {
//        if (list.ContainsKey(type))
//        {
//            list.Remove(type);
//            totalWeightUpdate();
//        }
//    }

//    public void Clear()
//    {
//        list = new Dictionary<T, float>();
//        total = 0;
//    }

//    public float Get(T type)
//    {
//        if (list.ContainsKey(type))
//        {
//            return list[type];
//        }

//        return 0;
//    }

//    public void totalWeightUpdate()
//    {
//        var temp = 0;

//        for (var e : list)
//        {
//            // temp += e;
//        }

//        total = temp;
//    }

//    public T GetValue(float value)
//    {
//        if (value < 0.f)
//        {
//            value = 0;
//        }

//        if (value > total) 
//        { 
//            value = total;
//        }

//        float temp = 0;
//        for(var e : list)
//        {
//            // temp += e;
//        }
//    }
//}
