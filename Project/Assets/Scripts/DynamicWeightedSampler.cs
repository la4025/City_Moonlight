using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class DynamicWeightedSampler<ValueType> : WeightedSampler<ValueType>
{
    private Dictionary<ValueType, float> weightFactorByKey = new Dictionary<ValueType, float>();            // Ȯ�� ������ ������
    private Dictionary<ValueType, float> decayFactorByKey = new Dictionary<ValueType, float>();            // Ȯ�� ������ ������

    // ���̳�! decay factor�� ���̺�� �������� ���ұ�!!

    public override void Add(ValueType type, float weight)
    {
        base.Add(type, weight);
        weightFactorByKey[type] = 1f;
    }
    public void Add(ValueType type, float weight, float decay)
    {
        Add(type, weight);
        decayFactorByKey[type] = decay;
    }
    public override void Remove(ValueType type)
    {
        base.Remove(type);
        weightFactorByKey.Remove(type);
        decayFactorByKey.Remove(type);
    }

    public override void Clear()
    {
        base.Clear();
        weightFactorByKey.Clear();
        decayFactorByKey.Clear();
    }

    public override float GetWeight(ValueType type)
    {
        if (weightFactorByKey.ContainsKey(type) && base.IsContain(type))
        {
            return (float)base.GetWeight(type) * (float)weightFactorByKey[type];
        }
        else if (!weightFactorByKey.ContainsKey(type) && base.IsContain(type))
        {
            return (float)base.GetWeight(type) * 1f;
        }
        else
        {
            return 0f;
        }
    }

    // public override float GetProbability(ValueType type)
    // {
    //     return base.GetProbability(type) * (float)weightFactorByKey[type];
    // }

    public override ValueType GetValue()
    {
        CacheProbability();

        var val = base.GetValue();
        weightFactorByKey[val] *= decayFactorByKey[val];
        // ���⼭ ���� ����ȭ�� ����� weightFactor�� ��Ȯ�� ������ �ذ��� �� ���� ���̴�.
        // �α׸� �Ἥ �� �غ��ÿ�.
        // ���� ����ȭ�� �� �Ǿ��ٸ�, weightFactor���� ��� ������ �� 1�� ���;� �Ѵ�.
        float exponentTotal = 0f;

        weightFactorByKey.Keys.ToList().ForEach(e =>
        {
            exponentTotal += Mathf.Log((float)weightFactorByKey[e], 2f);
        });
        exponentTotal *= -1f;
        float pivotFactor = Mathf.Pow(2, exponentTotal / weightFactorByKey.Count);

        weightFactorByKey.Keys.ToList().ForEach(e =>
        {
            weightFactorByKey[e] *= pivotFactor;
        });

        return val;
    }
}
