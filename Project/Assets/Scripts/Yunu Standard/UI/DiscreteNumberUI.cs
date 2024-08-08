using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteNumberUI : MonoBehaviour
{
    [SerializeField] LocalBoolBranch[] trueOnActivated;
    public void SetNumber(int number)
    {
        if (number > trueOnActivated.Length)
        {
            foreach (var each in trueOnActivated)
                if (!each.LocalBoolean)
                {
                    each.LocalBoolean = true;
                    each.Invoke();
                }
            return;
        }
        if (number <= 0)
        {
            foreach (var each in trueOnActivated)
                if (each.LocalBoolean)
                {
                    each.LocalBoolean = false;
                    each.Invoke();
                }
            return;
        }
        var index = number - 1;
        for (int i = 0; i < trueOnActivated.Length; i++)
        {
            if (i <= index && !trueOnActivated[i].LocalBoolean)
            {
                trueOnActivated[i].LocalBoolean = true;
                trueOnActivated[i].Invoke();
            }
            else if (i > index && trueOnActivated[i].LocalBoolean)
            {
                trueOnActivated[i].LocalBoolean = false;
                trueOnActivated[i].Invoke();
            }
        }
    }
}
