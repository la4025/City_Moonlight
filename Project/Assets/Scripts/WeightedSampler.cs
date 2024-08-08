using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 가중치 랜덤
/// </summary>
public class WeightedSampler<ValueType>
{
    private Dictionary<ValueType, float> weightByKey = new Dictionary<ValueType, float>();                 // 초기화에 쓰이는 데이터
    private Dictionary<ValueType, float> probabilityByKey = new Dictionary<ValueType, float>();            // 확률 참조할 데이터
    private float total = 0;

    public bool IsContain(ValueType type)
    {
        return weightByKey.ContainsKey(type);
    }

    public int Count()
    {
        return weightByKey.Count;
    }

    /// <summary>
    /// 맵에 모듈과 가중치를 넣는다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="weight"></param>
    public virtual void Add(ValueType type, float weight)
    {
        if (!weightByKey.ContainsKey(type))
        {
            weightByKey.Add(type, weight);
            TotalWeightUpdate();
        }
    }

    /// <summary>
    /// 맵에 모듈을 검색해서 삭제함
    /// </summary>
    /// <param name="type"></param>
    public virtual void Remove(ValueType type)
    {
        if (weightByKey.ContainsKey(type))
        {
            weightByKey.Remove(type);
            TotalWeightUpdate();
        }
    }

    /// <summary>
    /// 맵을 클리어 함.
    /// </summary>
    public virtual void Clear()
    {
        weightByKey.Clear();
        probabilityByKey.Clear();
        total = 0;
    }

    /// <summary>
    /// 모듈에 해당하는 가중치를 가져옴
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public virtual float GetWeight(ValueType type)
    {
        if (weightByKey.ContainsKey(type))
        {
            return weightByKey[type];
        }

        return 0;
    }

    /// <summary>
    /// 모듈에 해당하는 퍼센트를 가져옴
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public virtual float GetProbability(ValueType type)
    {
        //if (probabilityByKey.ContainsKey(type))
        //{
        return probabilityByKey[type];
        //}

        //return 0;
    }

    /// <summary>
    /// 가중치 합을 업데이트 함
    /// </summary>
    public void TotalWeightUpdate()
    {
        float temp = 0;

        foreach (var e in weightByKey)
        {
            temp += GetWeight(e.Key);
        }

        total = temp;
    }

    /// <summary>
    /// 정규화 가중치를 가지는 맵을 업데이트함
    /// </summary>
    public void CacheProbability()
    {
        TotalWeightUpdate();
        float inverseTotal = 1f / total;

        weightByKey.Keys.ToList().ForEach(e =>
        {
            probabilityByKey[e] = GetWeight(e) * inverseTotal;
            //UnityEngine.Debug.Log(e + "이 나타날 확률 : " + probabilityByKey[e]);
        });
    }

    /// <summary>
    /// 실수에 해당하는 모듈을 반환함
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual ValueType GetValue()
    {
        float value = UnityEngine.Random.Range(0f, 1f);

        if (value < 0)
        {
            value = 0;
        }

        if (value > 1f)
        {
            value = 1f;
        }

        float temp = 0;

        foreach (var e in probabilityByKey)
        {
            temp += GetProbability(e.Key);

            // 쌓인 값과 비교해서 쌓인 값이 크면 해당 객체를 반환
            if (temp >= value)
            {
                return e.Key;
            }
        }

        throw new Exception("모듈 번환 예외");
    }
}
