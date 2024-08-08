using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEntity<EntityType> : MonoBehaviour where EntityType : class
{
    public static Dictionary<string, EntityType> entityById = new Dictionary<string, EntityType>();
    [SerializeField] protected string id;
    [SerializeField] protected EntityType entity;
    protected virtual void Awake()
    {
        Debug.Log("awake called from global entity");
        if (entityById.ContainsKey(id))
        {
            if (entityById[id] == null)
                entityById[id]= entity;
            else
                Destroy(gameObject);
        }
        else
            entityById.Add(id, entity);

    }
    protected virtual void Reset()
    {
        entity = GetComponent<EntityType>();
    }
    protected virtual void OnValidate()
    {
        id = gameObject.name;
    }
    protected virtual void OnDestroy()
    {
        if (entityById[id] == entity)
            entityById[id] = null;
    }
}
