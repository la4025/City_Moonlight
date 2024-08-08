using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    public float speed;
    private bool isKinematic = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isKinematic)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F) && isKinematic)
        {
            isKinematic = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && !isKinematic)
        {
            isKinematic = true;
        }
    }
}
