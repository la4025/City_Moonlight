using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Building : MonoBehaviour
{
    public uint floorLevel;
    public BuildingModule firstFloor;
    public BuildingModule[] middlePrefabs;
    public BuildingModule[] topPrefabs;
    //private BuildingModule[] containingModules;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
#if UNITY_EDITOR
    private BuildingModule ExtendModule(BuildingModule module, float height)
    {

        var newObj = PrefabUtility.InstantiatePrefab(module.gameObject, transform);
        BuildingModule newOne = newObj.GetComponent<BuildingModule>();
        //BuildingModule newOne = PrefabUtility.InstantiatePrefab<BuildingModule>(module, transform);
        newOne.transform.localPosition = new Vector3(0, height, 0);
        newOne.transform.localRotation = Quaternion.identity;
        newOne.enabled = true;
        newOne.gameObject.name = "BuildingModule";
        return newOne;
    }
#endif
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Selection.Contains(gameObject))
            return;

        HashSet<BuildingModule> children = transform.GetComponentsInChildren<BuildingModule>().ToHashSet();
        foreach (var child in children)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                try
                {
                    DestroyImmediate(child.gameObject);
                }
                catch (Exception e)
                {

                }
            };
        }
        float yPos = 0;
        BuildingModule prefab = null;
        for (int floor = 0; floor < floorLevel; floor++)
        {
            if (floor == 0)
            {
                prefab = firstFloor;
            }
            else if (floor == floorLevel - 1)
            {
                prefab = topPrefabs[UnityEngine.Random.Range(0, topPrefabs.Length - 1)];
            }
            else
            {
                prefab = middlePrefabs[UnityEngine.Random.Range(0, middlePrefabs.Length - 1)];
            }
            var newOne = ExtendModule(prefab, yPos);
            yPos += newOne.end.localPosition.y;
        }
    }
#endif
}
