using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class StageModule : MonoBehaviour
{
    [Serializable]
    public struct NextModuleInfo
    {
        public StageModule prefab;
        public float encounterWeight;
        public float decayFactor;
    }
    public float length;
    public Transform playerTransform;
    public Transform start;
    public Transform end;
    public bool isTip = true;
    public NextModuleInfo[] nextModuleInfos;
    const float preBuildThreshold = 20;
    const float deleteThreshold = 20;

    private DynamicWeightedSampler<StageModule> dynamicWeightedSampler;

    static public void CreateFirstPiece(StageModule prefab, Vector3 position, Quaternion rotation)
    {
        StageModule newOne = Instantiate<StageModule>(prefab);
        if (prefab.dynamicWeightedSampler == null)
            prefab.InitWeightedSampler();
        newOne.dynamicWeightedSampler = prefab.dynamicWeightedSampler;

        newOne.transform.position = position;
        newOne.transform.rotation = rotation;
        newOne.enabled = true;
        newOne.playerTransform = Player.instance.transform;
        newOne.gameObject.name = "stageModule";
        newOne.isTip = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        isTip = true;
    }

    // Update is called once per frame
    void Update()
    {
        // �ʰ� ������ ������
        // UnityEngine.Debug.Log(Vector3.Dot(playerTransform.forward, (-playerTransform.position + transform.position)));

        if (Vector3.Dot(Player.instance.transform.forward,
            (end.transform.position - Player.instance.transform.position)) < -deleteThreshold)
        {
            // keyValuePairs.Remove(this);
            Destroy(gameObject);
        }

        //// ���࿡ �ʰ� ���� ���� ��ġ�� ����̰�
        //// �� ������ ī�޶� ���� ���볯 �� ������, ���� ����� �����ض�!
        if (isTip == true)
        {
            // ���� �÷����� �÷��̾�� �ڿ� ������, ���� �� �÷����� �����ؾ� �� ��, �� ������ �Ʒ��� �������� �Ǵ��Ѵ�.
            if (Vector3.Dot(Player.instance.transform.forward,
                (transform.position - Player.instance.transform.position)) < preBuildThreshold)
            {
                // ����� ����ġ�� ���� �����ǰ� �� ����.
                // ���� ����� ���� �� �������� �ʵ��� �����Ǹ� ����ġ�� ����.
                // ���� ����� 2�� �������� ������ ������ ����� ������ ����. (����ġ 0)
                // int index = UnityEngine.Random.Range(0, nextModuleInfos.Length);

                if (dynamicWeightedSampler is null)
                {
                    Debug.Log("temporary dynamicWeightedSamplre was created. if this log appears regularly, there must be something wrong.");
                    InitWeightedSampler();
                }

                var module = dynamicWeightedSampler.GetValue();

                //�����鿡�� ���� ������Ʈ�� ����
                ExtendModule(module);
            }
        }
    }
    private void ExtendModule(StageModule prefab)
    {
        StageModule newOne = Instantiate<StageModule>(prefab);
        if (prefab.dynamicWeightedSampler == null)
            prefab.InitWeightedSampler();
        newOne.dynamicWeightedSampler = prefab.dynamicWeightedSampler;
        // �÷��̾� ��ġ���� ����� ���� �Ÿ��� ����
        var centerToEnd = end.transform.position - transform.position;
        var centerToEndLength = Vector3.Dot(transform.forward, centerToEnd);

        // ���ο� ����� ���� �Ÿ��� ����
        var newStartToCenter = newOne.start.transform.position - newOne.transform.position;
        var newStartToCenterLength = Mathf.Abs(Vector3.Dot(transform.forward, newStartToCenter));

        // ���ο� ����� ��ġ��, ����� �� + ���ο� ����� ���� �Ÿ��� ��.
        var pos = transform.position + transform.forward *
            (centerToEndLength + newStartToCenterLength);

        newOne.transform.position = pos;
        newOne.transform.rotation = transform.rotation;

        newOne.enabled = true;
        newOne.playerTransform = playerTransform;
        newOne.gameObject.name = "stageModule";
        isTip = false;
    }
    private void InitWeightedSampler()
    {
        //Assert.IsTrue(gameObject.IsPrefabInstance(), "weightedSample initialization wasn't called from prefab instance! ������ ���̳�!!");
        dynamicWeightedSampler = new DynamicWeightedSampler<StageModule>();

        // �Ʒ� ���� �˸°� �����Ͻÿ�.
        // dynamicWeightedSampler.DecayFactor = DecayFactor;

        // contain���� ����� �Ÿ��� ���ϰ� �ߺ��� ����� ���̺� ���� ������ ����.
        // �ϴ� ���̺� ������ 1ȸ�� �̷������� ���ǹ��� �ɾ��
        if (dynamicWeightedSampler.Count() == 0)
        {
            foreach (var i in nextModuleInfos)
            {
                dynamicWeightedSampler.Add(i.prefab, i.encounterWeight, i.decayFactor);
            }
        }
    }
    private void OnValidate()
    {
        length = (start.position - end.position).magnitude;
    }
}
