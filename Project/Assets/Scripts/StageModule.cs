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
        // 너가 조건을 만들어라
        // UnityEngine.Debug.Log(Vector3.Dot(playerTransform.forward, (-playerTransform.position + transform.position)));

        if (Vector3.Dot(Player.instance.transform.forward,
            (end.transform.position - Player.instance.transform.position)) < -deleteThreshold)
        {
            // keyValuePairs.Remove(this);
            Destroy(gameObject);
        }

        //// 만약에 너가 가장 끝에 위치한 모듈이고
        //// 빈 공간이 카메라에 의해 들통날 것 같으면, 다음 모듈을 생성해라!
        if (isTip == true)
        {
            // 아직 플랫폼이 플레이어보다 뒤에 있지만, 슬슬 새 플랫폼을 생성해야 할 때, 그 시점을 아래의 조건으로 판단한다.
            if (Vector3.Dot(Player.instance.transform.forward,
                (transform.position - Player.instance.transform.position)) < preBuildThreshold)
            {
                // 모듈의 가중치에 따라 생성되게 할 예정.
                // 같은 모듈이 여러 번 생성되지 않도록 생성되면 가중치를 죽임.
                // 같은 모듈이 2번 연속으로 나오면 다음은 절대로 나오지 않음. (가중치 0)
                // int index = UnityEngine.Random.Range(0, nextModuleInfos.Length);

                if (dynamicWeightedSampler is null)
                {
                    Debug.Log("temporary dynamicWeightedSamplre was created. if this log appears regularly, there must be something wrong.");
                    InitWeightedSampler();
                }

                var module = dynamicWeightedSampler.GetValue();

                //프리펩에서 게임 오브젝트를 생성
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
        // 플레이어 위치에서 모듈의 끝의 거리를 구함
        var centerToEnd = end.transform.position - transform.position;
        var centerToEndLength = Vector3.Dot(transform.forward, centerToEnd);

        // 새로운 모듈의 전방 거리를 구함
        var newStartToCenter = newOne.start.transform.position - newOne.transform.position;
        var newStartToCenterLength = Mathf.Abs(Vector3.Dot(transform.forward, newStartToCenter));

        // 새로운 모듈의 위치를, 모듈의 끝 + 새로운 모듈의 전방 거리로 함.
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
        //Assert.IsTrue(gameObject.IsPrefabInstance(), "weightedSample initialization wasn't called from prefab instance! 정기훈 네이놈!!");
        dynamicWeightedSampler = new DynamicWeightedSampler<StageModule>();

        // 아래 줄을 알맞게 수정하시오.
        // dynamicWeightedSampler.DecayFactor = DecayFactor;

        // contain에서 제대로 거르지 못하고 중복된 모듈이 테이블에 들어가는 문제가 생김.
        // 일단 테이블 생성을 1회만 이뤄지도록 조건문을 걸어둠
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
