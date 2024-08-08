using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatContainerAggregate : ContainerAggregate<FloatContainer>
{
    public float sum
    {
        get
        {
            float _sum = 0;
            foreach (var each in containers)
            {
                _sum += each.Value;
            }
            return _sum;
        }
    }
    public float mean { get { return sum / containers.Length; } }
    [SerializeField]
    private FloatInterpolator.FloatSetter sumListener, meanListener;
    public override void Invoke()
    {
        sumListener.Invoke(sum);
        meanListener.Invoke(mean);
    }
}
