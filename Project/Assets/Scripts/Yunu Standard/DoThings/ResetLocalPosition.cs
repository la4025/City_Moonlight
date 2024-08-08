using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLocalPosition : MonoBehaviour
{
    [SerializeField] Vector3 originalLocalPosition;
    public void ResetPosition()
    {
        transform.localPosition = originalLocalPosition;
    }
}
