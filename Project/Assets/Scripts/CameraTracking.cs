using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public float lowest = 3;
    void Update()
    {
        float yPos = Mathf.Max(lowest, Player.instance.transform.position.y);
        transform.position += Vector3.up * (yPos - transform.position.y);
    }
}
