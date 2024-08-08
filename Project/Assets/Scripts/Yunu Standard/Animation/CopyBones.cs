using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CopyBones : MonoBehaviour
{
    [SerializeField]
    Transform originalRootBone;
    [SerializeField]
    Transform[] ignoreList;
    Dictionary<string, Transform> originalBones;
    Transform[] bones;
    HashSet<Transform> ignoreListSet;
    private void Start()
    {
        originalBones = originalRootBone.GetComponentsInChildren<Transform>().ToDictionary(x => x.gameObject.name, y => y);
        bones = transform.GetComponentsInChildren<Transform>();
        ignoreListSet = ignoreList.ToHashSet();
    }
    private void Update()
    {
        foreach (var each in bones)
        {
            if (ignoreListSet.Contains(each))
                continue;
            if (originalBones.ContainsKey(each.gameObject.name))
            {
                each.localRotation = originalBones[each.gameObject.name].localRotation;
                each.localPosition = originalBones[each.gameObject.name].localPosition;
            }
        }
    }
}
