using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingModule : MonoBehaviour
{
    // �������� ��⿡ �پ
    // ������(����)�� �޴� ������ �ǹ��� ���������� �� ����.
    // ���� ������ �´� ����� (���� ����) �������� �����ϴ� ���� �ݺ�.
    // 
    //[Serializable]
    //public struct NextModuleInfo
    //{
    //    public BuildingModule prefab;
    //    public float encounterWeight;
    //}
    //public uint floor;
    //private uint currFloor;

    //[SerializeField]
    //private float length;
    public Transform start;
    public Transform end;

    //public NextModuleInfo[] nextFloorModule;        // ���� ��

    //private WeightedSampler<BuildingModule> weightedSampler;


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{
    //    if (weightedSampler is null)
    //    {
    //        InitWeightedSampler();
    //    }

    //    var module = weightedSampler.GetValue();

    //    if (floor != currFloor && nextFloorModule.Length is not 0)
    //    {
    //        ExtendModule(module);
    //    }
    //}

    //private void InitWeightedSampler()
    //{
    //    weightedSampler = new WeightedSampler<BuildingModule>();

    //    if (weightedSampler.Count() is 0)
    //    {
    //        foreach (var modules in nextFloorModule)
    //        {
    //            weightedSampler.Add(modules.prefab, modules.encounterWeight);
    //        }
    //    }
    //    weightedSampler.CacheProbability();
    //}
    //private void ExtendModule(BuildingModule module)
    //{
    //    BuildingModule newOne = Instantiate<BuildingModule>(module);
    //    if (module.weightedSampler is null)
    //        module.InitWeightedSampler();
    //    newOne.weightedSampler = weightedSampler;
    //    // start = newOne.start; 
    //    // end = newOne.end;

    //    // �߽ɿ��� ���۱���. 
    //    var centerToEnd = end.transform.position - transform.position;
    //    var centerToEndLength = Vector3.Dot(transform.up, centerToEnd);

    //    var newStartToCenter = newOne.transform.position - newOne.start.transform.position;
    //    var newStartToCenterLength = Vector3.Dot(transform.up, newStartToCenter);

    //    var pos = transform.position + transform.up * (centerToEndLength + newStartToCenterLength);

    //    newOne.transform.position = pos;
    //    newOne.transform.rotation = transform.rotation;

    //    newOne.enabled = true;
    //    newOne.gameObject.name = "BuildingModule";

    //    newOne.floor = floor;
    //    newOne.currFloor = currFloor + 1;
    //}
    //private void OnValidate()
    //{
    //    length = (start.position - end.position).magnitude;
    //}
}
