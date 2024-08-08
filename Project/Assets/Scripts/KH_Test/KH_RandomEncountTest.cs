using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KH_RandomEncountTest : MonoBehaviour
{
    [Serializable]
    public struct TestModule
    {
        public int prefab;
        public float encounterWeight;
    }
    public TestModule[] nextModuleInfos;

    DynamicWeightedSampler<int> randomEncounter = new DynamicWeightedSampler<int>();

    // Start is called before the first frame update
    public void Test()
    {
        foreach (var i in nextModuleInfos)
        {
            randomEncounter.Add(i.prefab, i.encounterWeight);
        }

        for (int i = 0; i < 100; i++)
        {
            var e = randomEncounter.GetValue();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
