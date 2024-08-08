using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    public float timeScale
    {
        get { return Time.timeScale; }
        set { Time.timeScale = value; }
    }
}
