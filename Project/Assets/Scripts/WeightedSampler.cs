using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ����ġ ����
/// </summary>
public class WeightedSampler<ValueType>
{
    private Dictionary<ValueType, float> weightByKey = new Dictionary<ValueType, float>();                 // �ʱ�ȭ�� ���̴� ������
    private Dictionary<ValueType, float> probabilityByKey = new Dictionary<ValueType, float>();            // Ȯ�� ������ ������
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
    /// �ʿ� ���� ����ġ�� �ִ´�.
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
    /// �ʿ� ����� �˻��ؼ� ������
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
    /// ���� Ŭ���� ��.
    /// </summary>
    public virtual void Clear()
    {
        weightByKey.Clear();
        probabilityByKey.Clear();
        total = 0;
    }

    /// <summary>
    /// ��⿡ �ش��ϴ� ����ġ�� ������
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
    /// ��⿡ �ش��ϴ� �ۼ�Ʈ�� ������
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
    /// ����ġ ���� ������Ʈ ��
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
    /// ����ȭ ����ġ�� ������ ���� ������Ʈ��
    /// </summary>
    public void CacheProbability()
    {
        TotalWeightUpdate();
        float inverseTotal = 1f / total;

        weightByKey.Keys.ToList().ForEach(e =>
        {
            probabilityByKey[e] = GetWeight(e) * inverseTotal;
            //UnityEngine.Debug.Log(e + "�� ��Ÿ�� Ȯ�� : " + probabilityByKey[e]);
        });
    }

    /// <summary>
    /// �Ǽ��� �ش��ϴ� ����� ��ȯ��
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

            // ���� ���� ���ؼ� ���� ���� ũ�� �ش� ��ü�� ��ȯ
            if (temp >= value)
            {
                return e.Key;
            }
        }

        throw new Exception("��� ��ȯ ����");
    }
}
