using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using TMPro.EditorUtilities;
#endif
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
    public static StageManager instance { get; private set; }
    [SerializeField]
    public int StageLevel = 1;
    [SerializeField]
    private UnityEvent stage2TransitionEvent;
    [SerializeField]
    private UnityEvent stage3TransitionEvent;
    [SerializeField]
    private StageModule stage1InitialModule;
    [SerializeField]
    private StageModule stage2InitialModule;
    [SerializeField]
    private StageModule stage3InitialModule;
    [SerializeField]
    private int stage1RequirementsNum;
    [SerializeField]
    private int stage2RequirementsNum;
    [SerializeField]
    private int stage3RequirementsNum;
    [SerializeField]
    private int _stageRequirementsAccquired;
    public int stageRequirementsAccquired { get { return _stageRequirementsAccquired; } set { _stageRequirementsAccquired = value; } }

    public bool stageRequirementsMet()
    {
        int requirementsNeeded = 0;
        if (StageLevel == 1)
            requirementsNeeded = stage1RequirementsNum;
        if (StageLevel == 2)
            requirementsNeeded = stage2RequirementsNum;
        if (StageLevel == 3)
            requirementsNeeded = stage3RequirementsNum;
        return stageRequirementsAccquired >= requirementsNeeded;
    }

    private void Awake()
    {
        instance = this;
    }
    public void Proceed()
    {
        StageLevel++;
        if (StageLevel == 2)
            stage2TransitionEvent.Invoke();
        if (StageLevel == 3)
            stage3TransitionEvent.Invoke();
        stageRequirementsAccquired = 0;
        ResetStageModules();
    }
    public void ResetStageModules()
    {
        var remainderModules = FindObjectsByType<StageModule>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var eachModule in remainderModules)
            Destroy(eachModule.gameObject);
        Vector3 position = new Vector3(Player.instance.transform.position.x, 0, Player.instance.transform.position.z);
        StageModule prefab = null;

        if (StageLevel == 1)
            prefab = stage1InitialModule;
        if (StageLevel == 2)
            prefab = stage2InitialModule;
        if (StageLevel == 3)
            prefab = stage3InitialModule;

        StageModule.CreateFirstPiece(prefab, position, Quaternion.identity);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
