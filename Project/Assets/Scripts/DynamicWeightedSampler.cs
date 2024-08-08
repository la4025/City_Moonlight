using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class DynamicWeightedSampler<ValueType> : WeightedSampler<ValueType>
{
    private Dictionary<ValueType, float> weightFactorByKey = new Dictionary<ValueType, float>();            // 확률 참조할 데이터
    private Dictionary<ValueType, float> decayFactorByKey = new Dictionary<ValueType, float>();            // 확률 참조할 데이터

    // 네이놈! decay factor를 테이블로 저장하지 못할까!!

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
        // 여기서 지수 정규화를 해줘야 weightFactor의 정확도 문제를 해결할 수 있을 것이다.
        // 로그를 써서 잘 해보시오.
        // 지수 정규화가 잘 되었다면, weightFactor들을 모두 곱했을 때 1이 나와야 한다.
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
