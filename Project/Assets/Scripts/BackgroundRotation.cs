using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationFactor = 30;
    void Update()
    {
        transform.localRotation = Quaternion.Euler(
            0, 0, Player.instance.transform.position.z * rotationFactor);
    }
}
