using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContainerAggregate<ContainerType> : MonoBehaviour
{
    [SerializeField]
    protected ContainerType[] containers;
    //Define Invoke method to handle data groups value, such as statstic things like sum, mean, etc... 
    [SerializeField]
    public abstract void Invoke();
}
