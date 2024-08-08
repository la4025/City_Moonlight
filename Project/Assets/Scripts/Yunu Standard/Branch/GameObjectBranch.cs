using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectBranch : MonoBehaviour
{
    [Serializable]
    public class EventPerGameObject
    {
        public GameObject gameObject;
        public UnityEvent<GameObject> branchEvent;
    }
    [SerializeField] EventPerGameObject[] eventsPerGameObject;
    private Dictionary<GameObject, UnityEvent<GameObject>> eventsPerGameObjectDict = null;
    private void Start()
    {
        eventsPerGameObjectDict = new Dictionary<GameObject, UnityEvent<GameObject>>();
        foreach (var each in eventsPerGameObject)
            eventsPerGameObjectDict[each.gameObject] = each.branchEvent;
    }
    public void Invoke(GameObject gameObject)
    {
        if (eventsPerGameObjectDict != null)
        {
            if (eventsPerGameObjectDict.ContainsKey(gameObject))
                eventsPerGameObjectDict[gameObject].Invoke(gameObject);
        }
        else
        {
            foreach (var each in eventsPerGameObject)
                if (each.gameObject == gameObject)
                    each.branchEvent.Invoke(gameObject);
        }
    }
    public void Invoke(Component component)
    {
        Invoke(component.gameObject);
    }
}
