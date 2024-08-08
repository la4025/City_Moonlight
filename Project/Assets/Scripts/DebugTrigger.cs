using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTrigger : MonoBehaviour
{
    MeshRenderer pileRenderer;

    // Start is called before the first frame update
    void Start()
    {
        pileRenderer = gameObject.GetComponent<MeshRenderer>();
        pileRenderer.enabled = false;

#if DEBUG_MODE
        pileRenderer.enabled = true;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
